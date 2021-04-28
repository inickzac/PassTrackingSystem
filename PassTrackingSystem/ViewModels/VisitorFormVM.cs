using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class VisitorFormVM
    {   
        public Visitor Visitor { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<IssuingAuthority> IssuingAuthorities { get; set; }
    }
}
