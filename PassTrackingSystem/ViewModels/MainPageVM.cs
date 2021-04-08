using PassTrackingSystem.Models;
using PassTrackingSystem.Models.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class MainPageVM
    {
        private IGenericRepository<Visitor> visitorRep;
        public 
        public MainPageVM(IGenericRepository<Visitor> visitorRep)
        {
            this.visitorRep = visitorRep;
            Visitors = visitorRep.GetAll();
        }  
      public  IEnumerable<Visitor> Visitors { get; set; }   
    }
}
