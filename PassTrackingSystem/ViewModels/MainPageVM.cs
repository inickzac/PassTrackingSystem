using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using PassTrackingSystem.Models.SeparatorOnThePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class MainPageVM
    {
        private IGenericRepository<Visitor> visitorRep;
        public MainPageVM(IGenericRepository<Visitor> visitorRep, PageDividorOptions options=null)
        {            
            this.visitorRep = visitorRep;
            Visitors = new PagesDividedList<Visitor>(visitorRep.GetAll().OrderBy(v => v.Name),options);
        }  
      public  IEnumerable<Visitor> Visitors { get; set; }   
    }
}
