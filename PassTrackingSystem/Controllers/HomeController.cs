using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PassTrackingSystem.Infrastructure;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using PassTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Visitor> _visitorsRepository;
        private readonly ApplicationDBContext dBContext;
        private UserManager<AppUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger, 
            ApplicationDBContext dBContext,
             UserManager<AppUser> userManager,
             RoleManager<IdentityRole> roleManager,
            IGenericRepository<Visitor> visitorsRepository)
        {
            this.dBContext = dBContext;
            this.userManager= userManager;
            this.roleManager = roleManager;
            _visitorsRepository = visitorsRepository;
            _logger = logger;
        }
     
        public IActionResult Index(CommonListQuery commonListQuery)
        {
            new RandomDataGenerator(dBContext, userManager, roleManager);
            ViewBag.CurrentPage = "all-HomeController";
            return View(new MainPageVM(_visitorsRepository, commonListQuery) 
            { ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator") });              
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
