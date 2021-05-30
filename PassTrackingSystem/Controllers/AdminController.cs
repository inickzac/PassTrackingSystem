using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleMeneger;
        private readonly IGenericRepository<Employee> employeeRepository;
        public AdminController(UserManager<AppUser> userManager, IGenericRepository<Employee> employeeRepository,
            RoleManager<IdentityRole> roleMeneger)
        {
            this.roleMeneger = roleMeneger;
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Show(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                {
                    var user = await userManager.FindByIdAsync(Id.ToString());
                    if (user!=null)
                    {
                        var employee = employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).FirstOrDefault();
                        return View("AdminProcessing", new UserVM { AppUser = user, Employee = employee });  
                    }
                }
            }
            ViewBag.CurrentPage = "add-User";
            return View("AdminProcessing", new UserVM());
        }

        [HttpPost]
        public async Task<IActionResult> AdminProcessing(UserVM userVM)
        {
            if (userVM.AppUser.Id!="0" && !string.IsNullOrEmpty(userVM?.AppUser.Id))
            {
                var user = await userManager.FindByIdAsync(userVM.AppUser.Id);
                if (user!=null)
                {
                    await employeeRepository.Update(userVM.Employee);
                    user.UserName = userVM.AppUser.UserName;                  
                    user.EmployeeId = userVM.Employee.Id;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await UpdateRoleAsync(userVM.UserRole, user);
                        return View(userVM);
                    } 
                }
            }
            if (!string.IsNullOrEmpty(userVM.Password))
            {
                await employeeRepository.Update(userVM.Employee);
                userVM.AppUser.Id = Guid.NewGuid().ToString();
                userVM.AppUser.EmployeeId = userVM.Employee.Id;
                var result = await userManager.CreateAsync(userVM.AppUser, userVM.Password);
                if (result.Succeeded)
                {
                    await UpdateRoleAsync(userVM.UserRole, userVM.AppUser);
                    return View(userVM);
                }
            }
            return BadRequest();
        }

        public async Task<IActionResult> ShowAll(CommonListQuery options = null)
        {
            var PagesDividedQuery = Task.Run(() => new PagesDividedList<AppUser>(userManager.Users
                .SearchByMember(options.SearchСolumn, options.SearchValue)
                .OrderByMember("Id", true), options.CurrentPage, options.PageSize));

            var users = await PagesDividedQuery;
            ViewBag.CurrentPage = "all-User";
            var userEmployeePairs = users.Items.Select(u => new
            {
                employee = employeeRepository
                 .GetAll()
                 .Where(e => u.EmployeeId == e.Id)
                 .Include(v => v.Department)
                 .First(),
                user = u
            })
                 .ToDictionary(k => k.user, v => v.employee);
            return View(new UserVM { userEmployeePairs = userEmployeePairs, PageDividorInfo = users.PageDividorInfo });
        }

        private async Task UpdateRoleAsync(Role role, AppUser user)
        {
            await userManager.RemoveFromRolesAsync(user, new List<string> { "Administrator", "Moderator", "Operator" });
            if (role == Role.Administrator)
            {
                await userManager.AddToRolesAsync(user, new List<string> { "Administrator" });
            }
            if (role == Role.Moderator)
            {
                await userManager.AddToRolesAsync(user, new List<string> { "Moderator" });
            }
            if (role == Role.User)
            {
                await userManager.AddToRolesAsync(user, new List<string> { "Operator" });
            }
        }
    }
}
