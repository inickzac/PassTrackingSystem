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
    public class CarPassController : Controller
    {
        private readonly IGenericRepository<CarPass> passRepository;
        private readonly IGenericRepository<Employee> employeeRepository;
        private readonly UserManager<AppUser> userManager;
        public CarPassController(IGenericRepository<CarPass> passRepository,
            IGenericRepository<Employee> employeeRepository, UserManager<AppUser> userManager)
        {
            this.passRepository = passRepository;
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> CarPassProcessing(int id, int visitorId)
        {
            CarPass carPass;
            if (id != 0)
            {
                carPass = await passRepository.GetAll()
                    .Where(v => v.Id == id)
                    .Include(v => v.Car)
                    .Include(v => v.CarPassIssued)
                    .ThenInclude(v => v.Department)
                    .FirstAsync();
            }
            else
            {
                carPass = new CarPass();
                if (visitorId != 0)
                {
                    var user = await userManager.GetUserAsync(HttpContext.User);
                    var employee = await employeeRepository.Get(user.EmployeeId);
                    carPass.CarPassIssued = employee;
                    carPass.ValidWith = DateTime.Now;
                    carPass.ValitUntil = DateTime.Now;
                    carPass.VisitorId = visitorId;
                }
                else return new BadRequestResult();
            }
            return View(new CarPassVM
            {                
                ProcessingCarPass = carPass,
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
            });
        }

        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<RedirectToActionResult> CarPassProcessing(CarPass ProcessingCarPass)
        {
            ModelState.Remove("ProcessingCarPass.Car.Id");
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var employee = await employeeRepository.Get(user.EmployeeId);
                ProcessingCarPass.CarPassIssued = employee;
                await passRepository.Update(ProcessingCarPass);
                int id = ProcessingCarPass.Id;
                return RedirectToAction("CarPassProcessing", new { id = id }); 
            }
            return RedirectToAction(nameof(CarPassController.BadRedirectRequest),
                    nameof(VisitorFormController));
        }

        public async Task<IActionResult> ShowAll(int? visitorId, CommonListQuery options = null)
        {
            var query = visitorId == null ? passRepository.GetAll() : passRepository.GetAll()
                .Where(v => v.VisitorId == visitorId);
            var passes = Task.Run(() => new PagesDividedList<CarPass>(query
                .Include(v => v.Car)
                  .Include(v => v.CarPassIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));
            if (visitorId != 0)
            {
                ViewBag.CurrentPage = "all-CarPass";
            }
            var TemporaryPasses = await passes;
            return View(new CarPassVM { CarPasses = await passes, PurposeVisitorId = visitorId, 
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator") });
        }

        public async Task<IActionResult> Document(int passId, bool itsForDocument = true)
        {
            if (passId != 0)
            {
                var pass = await passRepository.GetAll()
                    .Where(p => p.Id == passId)
                    .Include(p => p.Visitor).ThenInclude(v => v.Document).ThenInclude(d => d.DocumentType)
                    .Include(p => p.Visitor).ThenInclude(v => v.Document).ThenInclude(d => d.IssuingAuthority)
                    .Include(p=> p.Car)
                    .FirstAsync();

                var documentVM = new CommonInformationVM
                {
                    Document = pass.Visitor.Document,
                    Id = pass.Id,
                    Name = pass.Visitor.Name,
                    LastName = pass.Visitor.LastName,
                    Patronymic = pass.Visitor.LastName,
                    PassType = "Транспортный пропуск",
                    ValidWith = pass.ValidWith.ToLongDateString(),
                    ValitUntil = pass.ValitUntil.ToLongDateString(),
                    PlaceOfWork = pass.Visitor.PlaceOfWork,
                    Position = pass.Visitor.Position,
                    PurposeOfIssuance = pass.PurposeOfIssuance,
                    Car = pass.Car
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
    }
}
