using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class DocumentGeneratorController : Controller
    {
        public IActionResult ShowTemporaryPassDocument()
        {
            return View();
        }
    }
}
