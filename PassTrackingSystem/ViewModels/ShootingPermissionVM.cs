using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class ShootingPermissionVM : ITableWithOptionsVM
    {
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
                 { "id", "Id" },
                { "Действует с",  nameof(ShootingPermission.ValidWith)},
                { "Действует до",  nameof(ShootingPermission.ValitUntil)},
                { "Пропуск выдал",  nameof(ShootingPermission.ShootingPermissionIssued)},
                { "Цель выдачи",  nameof(ShootingPermission.ShootingPurpose)},
                { "Места для посещения",  nameof(ShootingPermission.StationFacilities)},
            };
        public PagesDividedList<ShootingPermission> ShootingPermissions { get; set; }
        public ShootingPermission ProcessingShootingPermission { get; set; }
        public int? PurposeVisitorId { get; set; }
        public bool ShowAdvancedFeatures { get; set; }
    }
}
