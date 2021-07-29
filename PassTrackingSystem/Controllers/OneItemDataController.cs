using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class OneItemDataController : Controller
    {
        private readonly Dictionary<string, IoneValueActions> nameAndActionPairs;
        ApplicationDBContext applicationDBContext;

        public OneItemDataController(IGenericRepository<DocumentType> documentTypeRepository,
            IGenericRepository<IssuingAuthority> issuingAuthorityRepository, IGenericRepository<Department> departmentRepository)
        {
            var documentTypeOption = new IoneValueActions
            {
                GetAll = documentTypeRepository.GetAll().Select(v => v as IOneValueCommon),
                UpdateValue = (oneValueObject) => documentTypeRepository
                .Update(new DocumentType { Id = oneValueObject.Id, Value = oneValueObject.Value }),
                DeleteValue = (oneValueObject) => documentTypeRepository.Delete(oneValueObject.Id),
                OneValueName = "Тип документа"
            };

            var issuingAuthoritiesOption = new IoneValueActions
            {
                GetAll = issuingAuthorityRepository.GetAll().Select(v => v as IOneValueCommon),
                UpdateValue = (oneValueObject) => issuingAuthorityRepository.
                Update(new IssuingAuthority { Id = oneValueObject.Id, Value = oneValueObject.Value }),
                DeleteValue = (oneValueObject) => issuingAuthorityRepository.Delete(oneValueObject.Id),
                OneValueName = "Орган выдавший документ"
            };
            var departmentOption = new IoneValueActions
            {
                GetAll = departmentRepository.GetAll().Select(v => v as IOneValueCommon),
                UpdateValue = (oneValueObject) => departmentRepository.
                Update(new Department { Id = oneValueObject.Id, Value = oneValueObject.Value }),
                DeleteValue = (oneValueObject) => departmentRepository.Delete(oneValueObject.Id),
                OneValueName = "Подразделение"
            };

            nameAndActionPairs = new Dictionary<string, IoneValueActions>
            {
                {"DocumentType", documentTypeOption},
                {"IssuingAuthority", issuingAuthoritiesOption},
                 {"Department", departmentOption},
            };
        }

        public async Task<IActionResult> ShowOneItemData(string oneItemTypeName, CommonListQuery options, int selectedId)
        {
            if (nameAndActionPairs.ContainsKey(oneItemTypeName))
            {
                var query = nameAndActionPairs[oneItemTypeName].GetAll
                    .SearchByMember(options.SearchСolumn, options.SearchValue)
                    .OrderByMember("Id", true);
                var dividedList = await Task.Run(() => new PagesDividedList<IOneValueCommon>(query, options.CurrentPage, options.PageSize));
                if (!dividedList.Items.Where(v => v.Id == selectedId).Any() && selectedId != 0)
                {
                    dividedList.Items.Add(nameAndActionPairs[oneItemTypeName].GetAll
                        .Where(v => v.Id == selectedId).First());
                }
                return View(new OneItemDataVM
                {
                    OneValues = dividedList,
                    Options = options,
                    ExtendMenuHeader = nameAndActionPairs[oneItemTypeName].OneValueName,
                    SelectedItem = selectedId,
                    ShowAdvancedFeatures = HttpContext.User.IsInRole("Administrator") || HttpContext.User.IsInRole("Moderator")
                });
            }
            return BadRequest();
        }

        public async Task<IActionResult> ShowOneItems(string oneItemName)
        {
            if (nameAndActionPairs.ContainsKey(oneItemName))
            {
                return new JsonResult(await nameAndActionPairs[oneItemName].GetAll.ToListAsync());
            }
            return new JsonResult(new { BadRequest = "BadRequest" });
        }
        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<IActionResult> AddOneItem(string oneItemName, string value, int id)
        {
            if (nameAndActionPairs.ContainsKey(oneItemName) && !string.IsNullOrEmpty(value))
            {
                await nameAndActionPairs[oneItemName].UpdateValue(new OneValueCommon { Value = value, Id = id });
                return new OkObjectResult("success");
            }
            return new BadRequestResult();
        }

        [HttpPost, Authorize(Roles = "Moderator, Administrator")]
        public async Task<IActionResult> Delete(string oneItemName, int id)
        {
            if (nameAndActionPairs.ContainsKey(oneItemName))
            {
                await nameAndActionPairs[oneItemName].DeleteValue(new OneValueCommon { Id = id });
                return new OkObjectResult("success");
            }
            return new OkResult();
        }
        private class IoneValueActions
        {
            public IQueryable<IOneValueCommon> GetAll { get; set; }
            public Func<IOneValueCommon, Task> UpdateValue { get; set; }
            public Func<IOneValueCommon, Task> DeleteValue { get; set; }
            public string OneValueName { get; set; }
        }

    }
}
