using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using PassTrackingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class VisitorFormController : Controller
    {
        IGenericRepository<Visitor> visitorsRepository;
        IGenericRepository<DocumentType> documentTypesRepository;
        IGenericRepository<IssuingAuthority> issuingAuthoritiesRepository;

        public VisitorFormController(IGenericRepository<Visitor> visitorsRepository,
            IGenericRepository<DocumentType> documentTypesRepository,
            IGenericRepository<IssuingAuthority> issuingAuthoritiesRepository)
        {
            this.visitorsRepository = visitorsRepository;
            this.documentTypesRepository = documentTypesRepository;
            this.issuingAuthoritiesRepository = issuingAuthoritiesRepository;
        }

        [HttpGet]
        public async Task<ViewResult> VisitorProcessing(int id)
        {
            Visitor visitor;
            if (id != 0)
            {
                visitor = await visitorsRepository.GetAll()
                       .Where(v => v.Id == id)
                       .Include(v => v.Document)
                       .ThenInclude(i => i.IssuingAuthority)
                       .Include(v => v.Document.DocumentType)
                       .Include(v=> v.TemporaryPasses)
                       .Include(v => v.SinglePasses)
                       .Include(v => v.ShootingPermissions)
                       .Include(v => v.CarPasses).ThenInclude(v=> v.Car)
                       .FirstAsync();
            }

            else visitor = new Visitor();
            return View(new VisitorFormVM
            {
                Visitor = visitor,
            });
        }
        [HttpPost]
        public async Task<RedirectToActionResult> VisitorProcessing(Visitor visitor)
        {
            await visitorsRepository.Update(visitor);           
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }
    }
}
