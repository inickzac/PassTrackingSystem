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

        public async Task<IActionResult> LoginAsync(UserVM  userVM, string returnUrl)
        {
            var user=  userManager?.FindByNameAsync(userVM.AppUser.UserName );
            if(user!=null)
            {
              await signInManager.SignOutAsync();
              var signRes = await signInManager.PasswordSignInAsync(userVM.AppUser, userVM.Password, false, false);
                if(signRes.Succeeded)
                {
                    return Redirect(returnUrl);
                }
            }
            return View();
        }
    }
}
