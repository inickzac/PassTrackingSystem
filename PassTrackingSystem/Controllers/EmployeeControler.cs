using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class EmployeeControler : Controller
    {
        private readonly IGenericRepository<Employee> employeeRepository;

        public EmployeeControler(IGenericRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> ShowOneItems()
        {
            return new JsonResult( await employeeRepository.GetAll().ToListAsync());
        }
    }
}
