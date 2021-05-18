using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassTrackingSystem.Extensions;
using PassTrackingSystem.Infrastructure;
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
    public class TemporaryPassController : Controller
    {
        private readonly IGenericRepository<TemporaryPass> passRepository;
        private readonly IGenericRepository<StationFacility> stationFacilitysRepository;
        private readonly IGenericRepository<Employee> employeeRepository;
        public TemporaryPassController(IGenericRepository<TemporaryPass> passRepository,
            IGenericRepository<StationFacility> stationFacilitysRepository,
            IGenericRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.stationFacilitysRepository = stationFacilitysRepository;
            this.passRepository = passRepository;
        }

        public async Task<ViewResult> TemporaryPassProcessing(int id)
        {
            TemporaryPass temporaryPass;
            if (id != 0)
            {
                temporaryPass = await passRepository.GetAll()
                    .Where(v => v.Id == id)
                    .Include(v => v.StationFacilities)
                    .Include(v=> v.TemporaryPassIssued)
                    .ThenInclude(v=> v.Department)
                    .FirstAsync();
            }
            else
            {
                temporaryPass = new TemporaryPass();
                temporaryPass.ValidWith = DateTime.Now;
                temporaryPass.ValitUntil = DateTime.Now;
            }
            return View(new TemporaryPassVM
            {
                ProcessingTemporaryPass = temporaryPass,
            });
        }
        [HttpPost]
        public async Task<RedirectToActionResult> TemporaryPassProcessing(TemporaryPass ProcessingTemporaryPass, 
            List<int> facilitiesId)
        {
            await passRepository.Update(ProcessingTemporaryPass);
            passRepository.GetAll().Include(v => v.StationFacilities).
                Where(v => v.Id == ProcessingTemporaryPass.Id)
                .First();

            ProcessingTemporaryPass.StationFacilities = 
                await Task.Run(() => facilitiesId.Select(id => stationFacilitysRepository.GetAll()
               .Include(v => v.TemporaryPasses)
              .Where(v => v.Id == id).First()).ToList());
            
            await passRepository.Update(ProcessingTemporaryPass);
            GC.Collect();
            return RedirectToAction("ShowAll");
        }

        public async Task<IActionResult> ShowAll(CommonListQuery options = null)
        {
            var passes = Task.Run(() => new PagesDividedList<TemporaryPass>(passRepository.GetAll()
                  .Include(v => v.StationFacilities)
                  .Include(v => v.TemporaryPassIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));
            return View(new TemporaryPassVM { TemporaryPasses = await passes });
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
    
      
    }
}
