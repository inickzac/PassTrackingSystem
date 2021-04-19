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
        public List<string> HeadNames = new List<string>();
        public MainPageVM(IGenericRepository<Visitor> visitorRep, CommonListQuery options = null)
        {
            if (options == null) options = new CommonListQuery();
            Visitors = new PagesDividedList<Visitor>(visitorRep.GetAll()
                .OrderByMember("Id", false), options.CurrentPage, options.PageSize);
        }  
       
        public PagesDividedList<Visitor> Visitors { get; set; }   
    }
}
