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

        public async Task<ViewResult> VisitorProcessing(int id)
        {
            var visitor = visitorsRepository.GetAll()
                  .Where(v => v.Id == id)
                  .Include(v => v.Document)
                  .ThenInclude(i=> i.IssuingAuthority)
                  .Include(v=> v.Document.DocumentType)
                  .First();

            return  View (new VisitorFormVM { Visitor = visitor, 
                DocumentTypes = await documentTypesRepository.GetAll().ToListAsync(),
                IssuingAuthorities = await issuingAuthoritiesRepository.GetAll().ToListAsync() });
        }
    }
}
