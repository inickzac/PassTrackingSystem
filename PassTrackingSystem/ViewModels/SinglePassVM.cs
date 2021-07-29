using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class SinglePassVM : ITableWithOptionsVM
    {
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
               { "id", "Id" },
                { "Действует с",  nameof(SinglePass.ValidWith)},
                { "Действует до",  nameof(SinglePass.ValitUntil)},
                { "Пропуск выдал",  nameof(SinglePass.SinglePassIssued)},
                { "Цель выдачи",  nameof(SinglePass.PurposeOfIssuance)},
                { "Места для посещения",  nameof(SinglePass.StationFacilities)},
            };
        public PagesDividedList<SinglePass> SinglePasses { get; set; }
        public SinglePass ProcessingSinglePass { get; set; }
        public int? PurposeVisitorId { get; set; }
        public bool ShowAdvancedFeatures { get; set; }
    }
}
