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

        public async Task<IActionResult> TemporaryPassProcessing(int id, int visitorId)
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
                if (visitorId != 0)
                {
                    temporaryPass = new TemporaryPass();
                    temporaryPass.ValidWith = DateTime.Now;
                    temporaryPass.ValitUntil = DateTime.Now;
                    temporaryPass.VisitorId = visitorId;
                }
                else return new BadRequestResult();
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
            passRepository.GetAll().Where(v => v.Id == ProcessingTemporaryPass.Id).
                Include(v => v.StationFacilities)
                .First();

            ProcessingTemporaryPass.StationFacilities = 
                await Task.Run(() => facilitiesId.Select(id => stationFacilitysRepository.GetAll()
                .Where(v => v.Id == id)
               .Include(v => v.TemporaryPasses)
              .First()).ToList());
            
            await passRepository.Update(ProcessingTemporaryPass);
            int id = ProcessingTemporaryPass.Id;
            return RedirectToAction("TemporaryPassProcessing", new {id =id});
        }

        public async Task<IActionResult> ShowAll(int? visitorId ,CommonListQuery options = null)
        {
            var query = visitorId == null ? passRepository.GetAll() : passRepository.GetAll()
                .Where(v => v.VisitorId == visitorId);
            var passes = Task.Run(() => new PagesDividedList<TemporaryPass>(query
                  .Include(v => v.StationFacilities)
                  .Include(v => v.TemporaryPassIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));

            var TemporaryPasses = await passes;
            return View(new TemporaryPassVM { TemporaryPasses = await passes, PurposeVisitorId =visitorId });
        }

        public async Task<IActionResult> GetAllowedList(int processingPass)
        {
            var pass = await passRepository.GetAll()
                .Where(v => v.Id == processingPass)
                .Include(v => v.StationFacilities).FirstOrDefaultAsync();           
            if(pass==null)
            {
                pass = new TemporaryPass();
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
    
      
    }
}
