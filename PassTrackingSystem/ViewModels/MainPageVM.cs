using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassTrackingSystem.Extensions;

namespace PassTrackingSystem.ViewModels
{
    public class MainPageVM
    {
        public CommonListQuery Options { get; set; }
        public Dictionary<string, string> HeadNames { get; set; }
        public MainPageVM(IGenericRepository<Visitor> visitorRep, CommonListQuery options = null)
        {
            if (options == null) options = new CommonListQuery();
            Options = options;
            
            if (options.SearchСolumn!= "LNP")
            {
                Visitors = new PagesDividedList<Visitor>(visitorRep.GetAll()
                    .SearchByMember(options.SearchСolumn, options.SearchValue)
                       .OrderByMember("Id", false), options.CurrentPage, options.PageSize); 
            }
            
            HeadNames = new Dictionary<string, string> { { "id	", nameof(Visitor.Id) },
                { "ФИО", "LNP" },
                { "Место работы", nameof(Visitor.PlaceOfWork) },
                { "Должность", nameof(Visitor.Position) },
                { "Действующие пропуска", "" },
            };        
        } 
        public PagesDividedList<Visitor> Visitors { get; set; }
    }
}
