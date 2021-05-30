using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class CommonPassInfoController : Controller
    {
       public RedirectToActionResult CommonPassInfo(int searchId, int searchType)
        {
           if(searchId!=0)
            {
                switch (searchType)
                {
                    case 1:
                        return RedirectToAction(nameof(SinglePassController.Document), nameof(SinglePassController).Replace("Controller", ""), 
                            new {passId = searchId, itsForDocument= false });
                    case 2:
                        return RedirectToAction(nameof(TemporaryPassController.Document), nameof(TemporaryPassController).Replace("Controller", ""),
                             new { passId = searchId, itsForDocument = false });
                    case 3:
                        return RedirectToAction(nameof(CarPassController.Document), nameof(CarPassController).Replace("Controller", ""),
                             new { passId = searchId, itsForDocument = false });
                    case 4:
                        return RedirectToAction(nameof(ShootingPermissionController.Document), nameof(ShootingPermissionController).Replace("Controller", ""),
                             new { passId = searchId, itsForDocument = false });
                }
            }
            return RedirectToAction("error");
        } 

    }
}
