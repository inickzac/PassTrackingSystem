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
        private IGenericRepository<Visitor> visitorRep;
        public MainPageVM(IGenericRepository<Visitor> visitorRep, PageDividorOptions options=null)
        {
            if (options == null) options = new PageDividorOptions(nameof(Visitor.Id)) { IsOrderByDescending=true };
            this.visitorRep = visitorRep;
            Visitors = new PagesDividedList<Visitor>(visitorRep.GetAll()
                .OrderByMember(options.OrderPropertyName, options.IsOrderByDescending), options);
        }  
      public  IEnumerable<Visitor> Visitors { get; set; }   
    }
}
