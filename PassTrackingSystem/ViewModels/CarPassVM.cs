using PassTrackingSystem.Models;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class CarPassVM : ITableWithOptionsVM
    {
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; } =
            new Dictionary<string, string> {
                { "id", "Id" },
                { "Действует с",  nameof(CarPass.ValidWith)},
                { "Действует до",  nameof(CarPass.ValitUntil)},
                { "Пропуск выдал",  nameof(CarPass.CarPassIssued)},
                { "Цель выдачи",  nameof(CarPass.PurposeOfIssuance)},
                { "Номерной знак",  nameof(CarPass.Car.CarLicensePlate)},
                { "Марка автомобиля",  nameof(CarPass.Car.CarBrand)},
            };
        public PagesDividedList<CarPass> CarPasses { get; set; }
        public CarPass ProcessingCarPass{ get; set; }
        public int? PurposeVisitorId { get; set; }
        public bool ShowAdvancedFeatures { get; set; }
    }
}
