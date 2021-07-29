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
    public class SinglePassController : Controller
    {
        private readonly IGenericRepository<SinglePass> passRepository;
        private readonly IGenericRepository<StationFacility> stationFacilitysRepository;
        private readonly IGenericRepository<Employee> employeeRepository;
        private readonly UserManager<AppUser> userManager;
        public SinglePassController(IGenericRepository<SinglePass> passRepository,
            IGenericRepository<StationFacility> stationFacilitysRepository,
            IGenericRepository<Employee> employeeRepository, UserManager<AppUser> userManager)
        {
            this.employeeRepository = employeeRepository;
            this.stationFacilitysRepository = stationFacilitysRepository;
            this.passRepository = passRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> SinglePassProcessing(int id, int visitorId)
        {
            SinglePass singlePass;
            if (id != 0)
            {
                singlePass = await passRepository.GetAll()
                    .Where(v => v.Id == id)
                    .Include(v => v.StationFacilities)
                    .Include(v => v.SinglePassIssued)
                    .ThenInclude(v => v.Department)
                    .FirstAsync();
            }
            else
            {
                if (visitorId != 0)
                {
                    singlePass = new SinglePass();
                    singlePass.SinglePassIssued= await GetRequestUserAsync();
                    singlePass.ValidWith = DateTime.Now;
                    singlePass.ValitUntil = DateTime.Now;
                    singlePass.VisitorId = visitorId;
                }
                else return new BadRequestResult();

            }
            return View(new SinglePassVM
            {
                ProcessingSinglePass = singlePass,
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
            });
        }
        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<RedirectToActionResult> SinglePassProcessing(SinglePass ProcessingSinglePass,
            List<int> facilitiesId)
        {
            await passRepository.Update(ProcessingSinglePass);
            passRepository.GetAll().Include(v => v.StationFacilities).
                Where(v => v.Id == ProcessingSinglePass.Id)
                .First();

            ProcessingSinglePass.StationFacilities =
                await Task.Run(() => facilitiesId.Select(id => stationFacilitysRepository.GetAll()
               .Include(v => v.SinglePasses)
              .Where(v => v.Id == id).First()).ToList());
            ProcessingSinglePass.SinglePassIssued = await GetRequestUserAsync();
            await passRepository.Update(ProcessingSinglePass);
            int id = ProcessingSinglePass.Id;
            return RedirectToAction("SinglePassProcessing", new { id = id });
        }

        public async Task<IActionResult> ShowAll(int? visitorId, CommonListQuery options = null)
        {
            var query = visitorId == null ? passRepository.GetAll() : passRepository.GetAll()
                .Where(v => v.VisitorId == visitorId);
            if (options.SearchСolumn == nameof(ShootingPermission.StationFacilities))
            {
                query = query.Where(t => t.StationFacilities.Any(f => f.Value.Contains(options.SearchValue)));
                options.SearchСolumn = "";
            }
            var passes = Task.Run(() => new PagesDividedList<SinglePass>(query
                  .Include(v => v.StationFacilities)
                  .Include(v => v.SinglePassIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));
            if (visitorId != 0)
            {
                ViewBag.CurrentPage = "all-SinglePass";
            }
            var singlePasses = await passes;
            return View(new SinglePassVM { SinglePasses = await passes, PurposeVisitorId = visitorId,
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
            });
        }

        public async Task<IActionResult> GetAllowedList(int processingPass)
        {
            var pass = await passRepository.GetAll()
                .Where(v => v.Id == processingPass)
                .Include(v => v.StationFacilities).FirstOrDefaultAsync();
            if (pass == null)
            {
                pass = new SinglePass();
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

        public async Task<IActionResult> Document(int passId, bool itsForDocument=true)
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
                    PassType = "Разовый пропуск",
                    ValidWith = pass.ValidWith.ToString("dd:mm  yyyy-hh-mm"),
                    ValitUntil = pass.ValitUntil.ToString("dd:mm  yyyy-hh-mm"),
                    PlaceOfWork = pass.Visitor.PlaceOfWork,
                    Position = pass.Visitor.Position,
                    PurposeOfIssuance = pass.PurposeOfIssuance,
                    StationFacilities = pass.StationFacilities.Select(f => f.Value).ToList()
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
