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
        public TemporaryPassController(IGenericRepository<TemporaryPass> passRepository)
        {
            this.passRepository = passRepository;
        }

        public async Task<ViewResult> TemporaryPassProcessing(int id)
        {
            TemporaryPass temporaryPass;
            if (id != 0)
            {
                temporaryPass = await passRepository.GetAll()
                    .Where(v => v.Id == id).FirstAsync();
            }

            else temporaryPass = new TemporaryPass();
            return View(new TemporaryPassVM
            {
                
            });
        }
        [HttpPost]
        public async Task<RedirectToActionResult> TemporaryPassProcessing(TemporaryPass temporaryPass)
        {
          await passRepository.Update(temporaryPass);
          return  RedirectToAction("ShowAll");
        }
    
        public async Task<IActionResult> ShowAll(CommonListQuery options = null)
        {
            var passes=Task.Run(() => new PagesDividedList<TemporaryPass>(passRepository.GetAll()
                .Include(v=> v.StationFacilities)
                .Include(v=> v.TemporaryPassIssued)
                .ThenInclude(v=> v.Department)
                   .SearchByMember(options.SearchСolumn, options.SearchValue)
                      .OrderByMember("Id", true), options.CurrentPage, options.PageSize));
            return View(new TemporaryPassVM { TemporaryPasses =await passes });
        }
    }
}
