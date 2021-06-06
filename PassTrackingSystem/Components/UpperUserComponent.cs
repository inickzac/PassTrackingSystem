using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PassTrackingSystem.Components
{
    public class UpperUserComponent : ViewComponent
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IGenericRepository<Employee> employeeRepository;

        public UpperUserComponent(UserManager<AppUser> userManager, IGenericRepository<Employee> employeeRepository)
        {
            this.userManager = userManager;
            this.employeeRepository = employeeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var employee = await employeeRepository.Get(user.EmployeeId);
            var nameString = employee.LastName + " " + employee.Name + " " + employee.Patronymic;
            return View("/Views/Components/UpperUserComponent/UserUpperBlock.cshtml", nameString);
        }
    }
}
