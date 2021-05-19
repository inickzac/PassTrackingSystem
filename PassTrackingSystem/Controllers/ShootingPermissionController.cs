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
    public class ShootingPermissionController : Controller
    {
        private readonly IGenericRepository<ShootingPermission> passRepository;
        private readonly IGenericRepository<StationFacility> stationFacilitysRepository;
        private readonly IGenericRepository<Employee> employeeRepository;

        public ShootingPermissionController(IGenericRepository<ShootingPermission> passRepository,
            IGenericRepository<StationFacility> stationFacilitysRepository, 
            IGenericRepository<Employee> employeeRepository)
        {
            this.passRepository = passRepository;
            this.stationFacilitysRepository = stationFacilitysRepository;
            this.employeeRepository = employeeRepository;
        }

        public async Task<ViewResult> ShootingPermissionProcessing(int id)
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
                shootingPermission = new ShootingPermission();
                shootingPermission.ValidWith = DateTime.Now;
                shootingPermission.ValitUntil = DateTime.Now;
            }
            return View(new ShootingPermissionVM
            {
                ProcessingShootingPermission = shootingPermission,
            });
        }

        [HttpPost]
        public async Task<RedirectToActionResult> ShootingPermissionProcessing(ShootingPermission ProcessingShootingPermission,
            List<int> facilitiesId)
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

            await passRepository.Update(ProcessingShootingPermission);
            return RedirectToAction("ShootingPermissionProcessing", ProcessingShootingPermission.Id);
        }

        public async Task<IActionResult> GetAllowedList(int processingPass)
        {
            var pass = await passRepository.GetAll()
                .Where(v => v.Id == processingPass)
                .Include(v => v.StationFacilities).FirstAsync();
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
            return View(new ShootingPermissionVM { ShootingPermissions = await passes, PurposeVisitorId = visitorId });
        }

    }
}
