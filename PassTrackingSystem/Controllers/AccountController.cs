using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassTrackingSystem.Models;
using PassTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

       [HttpPost]
        public async Task<IActionResult> Login(UserVM  userVM, string returnUrl)
        {
            var user= await userManager?.FindByNameAsync(userVM.AppUser.UserName );
            if(user!=null)
            {
              await signInManager.SignOutAsync();
              var signRes = await signInManager.PasswordSignInAsync(user, userVM.Password, false, false);
                if(signRes.Succeeded)
                {
                    return Redirect(returnUrl);
                }
            }
            return View();
        }
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
    }
}
