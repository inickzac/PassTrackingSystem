using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class TemporaryPassVM : ITableWithOptionsVM
    {
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
                { "id", "Id" },
                { "Действует с",  nameof(TemporaryPass.ValidWith)},
                { "Действует до",  nameof(TemporaryPass.ValitUntil)},
                { "Пропуск выдал",  nameof(TemporaryPass.TemporaryPassIssued)},
                { "Цель выдачи",  nameof(TemporaryPass.PurposeOfIssuance)},
                { "Места для посещения",  nameof(TemporaryPass.StationFacilities)},
            };
        public PagesDividedList<TemporaryPass> TemporaryPasses { get; set; }
        public TemporaryPass ProcessingTemporaryPass { get; set; }
        public int? PurposeVisitorId { get; set; }
        public bool ShowAdvancedFeatures { get; set; }
    }
}
