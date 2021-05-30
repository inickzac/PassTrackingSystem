using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class GenerateDocumentVM
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = "Филиал ОАО ГТД";
        public string PassType { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PlaceOfWork { get; set; }
        public string Position { get; set; }
        public Document Document { get; set; }
        public string ValidWith { get; set; }
        public string ValitUntil { get; set; }
        public string PurposeOfIssuance { get; set; }
        public List<string> StationFacilities { get; set; }
        public Car Car { get; set; }
        public string CameraType { get; set; }
    }
}
