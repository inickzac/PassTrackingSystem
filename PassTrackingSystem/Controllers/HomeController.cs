﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PassTrackingSystem.Infrastructure;
using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _dBContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext dBContext)
        {
            _logger = logger;
            _dBContext = dBContext;
        }

        public IActionResult Index()
        {
            var r = new RandomDataGenerator(_dBContext);
            r.CreateVisitors(100);
            return View();
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
