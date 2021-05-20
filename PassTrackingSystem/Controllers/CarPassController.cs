﻿using Microsoft.AspNetCore.Mvc;
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
    public class CarPassController : Controller
    {
        private readonly IGenericRepository<CarPass> passRepository;
        private readonly IGenericRepository<Employee> employeeRepository;

        public CarPassController(IGenericRepository<CarPass> passRepository,
            IGenericRepository<Employee> employeeRepository)
        {
            this.passRepository = passRepository;
            this.employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> CarPassProcessing(int id, int visitorId)
        {
            CarPass carPass;
            if (id != 0)
            {
                carPass = await passRepository.GetAll()
                    .Where(v => v.Id == id)
                    .Include(v => v.Car)
                    .Include(v => v.CarPassIssued)
                    .ThenInclude(v => v.Department)
                    .FirstAsync();
            }
            else
            {
                carPass = new CarPass();
                if (visitorId != 0)
                {
                    carPass.ValidWith = DateTime.Now;
                    carPass.ValitUntil = DateTime.Now;
                    carPass.VisitorId = visitorId;
                }
                else return new BadRequestResult();
            }
            return View(new CarPassVM
            {
                ProcessingCarPass = carPass,
            });
        }

        [HttpPost]
        public async Task<RedirectToActionResult> CarPassProcessing(CarPass ProcessingCarPass)
        {
            await passRepository.Update(ProcessingCarPass);
            int id = ProcessingCarPass.Id;
            return RedirectToAction( "VisitorProcessing", "VisitorForm", new { id = id });
        }

        public async Task<IActionResult> ShowAll(int? visitorId, CommonListQuery options = null)
        {
            var query = visitorId == null ? passRepository.GetAll() : passRepository.GetAll()
                .Where(v => v.VisitorId == visitorId);
            var passes = Task.Run(() => new PagesDividedList<CarPass>(query
                .Include(v => v.Car)
                  .Include(v => v.CarPassIssued)
                  .ThenInclude(v => v.Department)
                     .SearchByMember(options.SearchСolumn, options.SearchValue)
                        .OrderByMember("Id", true), options.CurrentPage, options.PageSize));

            var TemporaryPasses = await passes;
            return View(new CarPassVM { CarPasses = await passes, PurposeVisitorId = visitorId });
        }
    }
}
