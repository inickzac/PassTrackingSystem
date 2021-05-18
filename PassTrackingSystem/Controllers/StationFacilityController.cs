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
    public class StationFacilityController : Controller
    {
        private readonly IGenericRepository<StationFacility> stationFacilityRep;
        public StationFacilityController(IGenericRepository<StationFacility> stationFacilityRep)
        {
            this.stationFacilityRep = stationFacilityRep;
        }

        public async Task<IActionResult> StationFacilityExtension(CommonListQuery options = null)
        {
            var passes =await Task.Run(() => new PagesDividedList<StationFacility>(stationFacilityRep.GetAll()
                    .SearchByMember(options.SearchСolumn, options.SearchValue)
                       .OrderByMember("Id", true), options.CurrentPage, options.PageSize));
            return View();
        }
      
    }
}
