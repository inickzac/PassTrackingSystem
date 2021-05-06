﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassTrackingSystem.Extensions;
using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using PassTrackingSystem.Models.SeparatorOnThePage;
using PassTrackingSystem.ViewModels;
using PassTrackingSystem.ViewModels.OneItemComboBoxVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Controllers
{
    public class OneItemDataController : Controller
    {
        private readonly Dictionary<string, IoneValueActions> nameAndActionPairs;

        public OneItemDataController(IGenericRepository<DocumentType> documentTypeRepository, 
            IGenericRepository<IssuingAuthority> issuingAuthorityRepository)
        {
            var documentTypeOption = new IoneValueActions
            {
                GetAll = documentTypeRepository.GetAll().Select(v => v as IOneValueCommon),
                AddValue = (oneValueObject) => documentTypeRepository.Update(oneValueObject as DocumentType),
                DeleteValue = (oneValueObject) => documentTypeRepository.Delete(oneValueObject.Id),
                OneValueName ="Тип документа"
            };

            var issuingAuthoritiesOption = new IoneValueActions
            {
                GetAll = issuingAuthorityRepository.GetAll().Select(v => v as IOneValueCommon),
                AddValue = (oneValueObject) => issuingAuthorityRepository.Update(oneValueObject as IssuingAuthority),
                DeleteValue = (oneValueObject) => issuingAuthorityRepository.Delete(oneValueObject.Id),
                OneValueName ="Орган выдавший документ"
            };

            nameAndActionPairs = new Dictionary<string, IoneValueActions>
            {
                {"DocumentType", documentTypeOption},
                {"IssuingAuthority", issuingAuthoritiesOption}
            };
        }

        public async Task<IActionResult> ShowOneItemData(string oneItemTypeName, CommonListQuery options)
        {
            if (nameAndActionPairs.ContainsKey(oneItemTypeName))
            {
                var query = nameAndActionPairs[oneItemTypeName].GetAll;
                var dividedList  = await Task.Run(() => new PagesDividedList<IOneValueCommon>(query, options.CurrentPage, options.PageSize));
                return View(new OneItemDataVM { OneValues =  dividedList, Options = options, 
                    ExtendMenuHeader= nameAndActionPairs[oneItemTypeName].OneValueName });
            }
            return BadRequest();
        }

        public async Task<JsonResult> ShowOneItems(string oneItemName)
        {
            if (nameAndActionPairs.ContainsKey(oneItemName))
            {
              return new JsonResult(await nameAndActionPairs[oneItemName].GetAll.ToListAsync());
            }
            return new JsonResult(new {BadRequest = "BadRequest" });
        }

        private class IoneValueActions
        {
            public IQueryable<IOneValueCommon> GetAll { get; set; }
            public Action<IOneValueCommon> AddValue { get; set; }
            public Action<IOneValueCommon> DeleteValue { get; set; }
            public string OneValueName { get; set; }
        }

    }
}
