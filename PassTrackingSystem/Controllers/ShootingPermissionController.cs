using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassTrackingSystem.Extensions;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using PassTrackingSystem.Models.SeparatorOnThePage;
using PassTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    [Authorize]
    public class ShootingPermissionController : Controller
    {
        private readonly IGenericRepository<ShootingPermission> passRepository;
        private readonly IGenericRepository<StationFacility> stationFacilitysRepository;
        private readonly IGenericRepository<Employee> employeeRepository;
        private readonly UserManager<AppUser> userManager;

        public ShootingPermissionController(IGenericRepository<ShootingPermission> passRepository,
            IGenericRepository<StationFacility> stationFacilitysRepository, UserManager<AppUser> userManager,
            IGenericRepository<Employee> employeeRepository)
        {
            this.passRepository = passRepository;
            this.stationFacilitysRepository = stationFacilitysRepository;
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ShootingPermissionProcessing(int id, int visitorId)
        {
            ShootingPermission shootingPermission;
            if (id != 0)
            {
                shootingPermission = await passRepository.GetAll()
                    .Where(v => v.Id == id)
                    .Include(v => v.StationFacilities)
                    .Include(v => v.ShootingPermissionIssued)
                    .ThenInclude(v => v.Department)
                    .FirstAsync();
            }
            else
            {
                if (visitorId != 0)
                {                    
                    shootingPermission = new ShootingPermission();
                    shootingPermission.ValidWith = DateTime.Now;
                    shootingPermission.ValitUntil = DateTime.Now;
                    shootingPermission.VisitorId = visitorId;
                    shootingPermission.ShootingPermissionIssued =await GetRequestUserAsync();
                }
                else return new BadRequestResult();

            }
            return View(new ShootingPermissionVM
            {
                ProcessingShootingPermission = shootingPermission,
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
            });
        }

        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<RedirectToActionResult> ShootingPermissionProcessing(ShootingPermission ProcessingShootingPermission,
            List<int> facilitiesId)
        {
            if (ModelState.IsValid)
            {
                await passRepository.Update(ProcessingShootingPermission);
                passRepository.GetAll().Where(v => v.Id == ProcessingShootingPermission.Id).
                    Include(v => v.StationFacilities)
                    .First();

                ProcessingShootingPermission.StationFacilities =
                    await Task.Run(() => facilitiesId.Select(id => stationFacilitysRepository.GetAll()
                    .Where(v => v.Id == id)
                   .Include(v => v.ShootingPermissions)
                  .First()).ToList());

                ProcessingShootingPermission.ShootingPermissionIssued = await GetRequestUserAsync();
                await passRepository.Update(ProcessingShootingPermission);
                int id = ProcessingShootingPermission.Id;
                return RedirectToAction("ShootingPermissionProcessing", new { id = id });
            }
            return RedirectToAction(nameof(CarPassController.BadRedirectRequest),
                       nameof(VisitorFormController));
        }

        public async Task<IActionResult> GetAllowedList(int processingPass)
        {
            var pass = await passRepository.GetAll()
                .Where(v => v.Id == processingPass)
                .Include(v => v.StationFacilities).FirstOrDefaultAsync();
            if (pass == null)
            {
                pass = new ShootingPermission();
                pass.StationFacilities = new List<StationFacility>();

            }
            var allPass = await stationFacilitysRepository.GetAll().ToListAsync();
            var accessPairs = allPass.Select(s => new
            {
                Facility = s,
                Allow = pass.StationFacilities
                .Where(v => v.Id == s.Id)
                .Any()
            })
                .ToDictionary(v => v.Facility, i => i.Allow);
            return View(accessPairs);
        }

        public async Task<IActionResult> ShowAll(int? visitorId, CommonListQuery options = null)
        {
            var query = visitorId == null ? passRepository.GetAll() : passRepository.GetAll()
                .Where(v => v.VisitorId == visitorId);
            var passes = Task.Run(() => new PagesDividedList<ShootingPermission>(query
                  .Include(v => v.StationFacilities)
                  .Include(v => v.ShootingPermissionIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));

            var TemporaryPasses = await passes;
            if (visitorId != 0)
            {
                ViewBag.CurrentPage = "all-ShootingPermission";
            }
            return View(new ShootingPermissionVM
            {
                ShootingPermissions = await passes,
                PurposeVisitorId = visitorId,
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
            });
        }

        public async Task<IActionResult> Document(int passId, bool itsForDocument = true)
        {
            if (passId != 0)
            {
                var pass = await passRepository.GetAll()
                    .Where(p => p.Id == passId)
                    .Include(p => p.Visitor).ThenInclude(v => v.Document).ThenInclude(d => d.DocumentType)
                    .Include(p => p.Visitor).ThenInclude(v => v.Document).ThenInclude(d => d.IssuingAuthority)
                    .Include(p => p.StationFacilities)
                    .FirstAsync();

                var documentVM = new CommonInformationVM
                {
                    Document = pass.Visitor.Document,
                    Id = pass.Id,
                    Name = pass.Visitor.Name,
                    LastName = pass.Visitor.LastName,
                    Patronymic = pass.Visitor.LastName,
                    PassType = "Разрешение на съемку",
                    ValidWith = pass.ValidWith.ToString("dd:mm  yyyy-hh-mm"),
                    ValitUntil = pass.ValitUntil.ToString("dd:mm  yyyy-hh-mm"),
                    PlaceOfWork = pass.Visitor.PlaceOfWork,
                    Position = pass.Visitor.Position,
                    PurposeOfIssuance = pass.ShootingPurpose,
                    StationFacilities = pass.StationFacilities.Select(f => f.Value).ToList(),
                    CameraType = pass.CameraType
                };
                if (itsForDocument)
                {
                    return View("Document", documentVM);
                }
                if (!itsForDocument)
                {
                    return View("CommonPassInfo", documentVM);
                }
            }
            return BadRequest();
        }

        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await passRepository.Delete(id);
            }
            return new OkResult();
        }

        public IActionResult BadRedirectRequest() => BadRequest();
    
        private async Task<Employee> GetRequestUserAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            return await employeeRepository.Get(user.EmployeeId);
        }
    }
}
