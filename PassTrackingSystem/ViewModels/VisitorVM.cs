using PassTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.ViewModels
{
    public class VisitorVM
    {
        [Required]
        Visitor Visitor { get; set; }
        [Required]
        Document Document { get; set; }

    }
}
