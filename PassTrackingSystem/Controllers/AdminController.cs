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
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private readonly IGenericRepository<Employee> employeeRepository;
        public AdminController(UserManager<AppUser> userManager, IGenericRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Show(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var user = await userManager.FindByIdAsync(Id.ToString());
                var employee = employeeRepository.GetAll().Where(v => v.Id == user.EmployeeId).FirstOrDefault();
                return View("AddUser", new UserVM { Name = user?.UserName, Employee = employee });
            }
            return View("AddUser", new UserVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            await employeeRepository.Update(userVM.Employee);
            var appUser = new AppUser { UserName = userVM.Name, EmployeeId = userVM.Employee.Id };
            var result = await userManager.CreateAsync(appUser, userVM.Password);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }

        public async Task<IActionResult> ShowAll(CommonListQuery options = null)
        {
            var PagesDividedQuery = Task.Run(() => new PagesDividedList<AppUser>(userManager.Users
                .SearchByMember(options.SearchСolumn, options.SearchValue)
                .OrderByMember("Id", true), options.CurrentPage, options.PageSize));

            var users = await PagesDividedQuery;
            var userEmployeePairs = users.Items.Select(u => new
            {
                employee = employeeRepository
                 .GetAll()
                 .Where(e => u.EmployeeId == e.Id)
                 .First(),
                user = u
            })
                 .ToDictionary(k => k.user, v =>  v.employee);
            return View(new UserVM { userEmployeePairs = userEmployeePairs });
        }
    }
}
