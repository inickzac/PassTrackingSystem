using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                       .Include(v => v.TemporaryPasses)
                       .Include(v => v.SinglePasses)
                       .Include(v => v.ShootingPermissions)
                       .Include(v => v.CarPasses).ThenInclude(v => v.Car)
                       .FirstAsync();
            }
            else
            {
                visitor = new Visitor();
                ViewBag.CurrentPage = "add-VisitorForm";
            }
            return View(new VisitorFormVM
            {
                Visitor = visitor,
                ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
            });
        }
        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<RedirectToActionResult> VisitorProcessing(Visitor visitor)
        {
            ModelState.Remove("Visitor.Document.Id");
            if (ModelState.IsValid)
            {
                await visitorsRepository.Update(visitor);
                return RedirectToAction(nameof(VisitorFormController.VisitorProcessing), new { id = visitor.Id });
            }

            return RedirectToAction(nameof(VisitorFormController.BadRedirectRequest),
                nameof(VisitorFormController));
        }

        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            if (id != 0)
            {
                await visitorsRepository.Delete(id);
            }
            return new OkResult();
        }

        public IActionResult BadRedirectRequest() => BadRequest();
    }
}
