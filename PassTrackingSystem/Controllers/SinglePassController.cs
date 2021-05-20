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
    public class SinglePassController : Controller
    {
        private readonly IGenericRepository<SinglePass> passRepository;
        private readonly IGenericRepository<StationFacility> stationFacilitysRepository;
        private readonly IGenericRepository<Employee> employeeRepository;
        public SinglePassController(IGenericRepository<SinglePass> passRepository,
            IGenericRepository<StationFacility> stationFacilitysRepository,
            IGenericRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.stationFacilitysRepository = stationFacilitysRepository;
            this.passRepository = passRepository;
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
                    singlePass.ValidWith = DateTime.Now;
                    singlePass.ValitUntil = DateTime.Now;
                    singlePass.VisitorId = visitorId;
                }
                else return new BadRequestResult();
                
            }
            return View(new SinglePassVM
            {
                ProcessingSinglePass = singlePass,
            });
        }
        [HttpPost]
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

            await passRepository.Update(ProcessingSinglePass);
            int id = ProcessingSinglePass.Id;
            return RedirectToAction("SinglePassProcessing",  new { id = id });
        }

        public async Task<IActionResult> ShowAll(int? visitorId, CommonListQuery options = null)
        {
            var query = visitorId == null ? passRepository.GetAll() : passRepository.GetAll()
                .Where(v => v.VisitorId == visitorId);
            var passes = Task.Run(() => new PagesDividedList<SinglePass>(query
                  .Include(v => v.StationFacilities)
                  .Include(v => v.SinglePassIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));

            var singlePasses = await passes;
            return View(new SinglePassVM { SinglePasses = await passes, PurposeVisitorId = visitorId });
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
    }
}
