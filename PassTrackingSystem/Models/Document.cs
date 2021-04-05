using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Models
{
    public class Document
    {
        public int Id { get; set; }
        [Required]
        public string Series { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public  DocumentType DocumentType { get; set; }
        [Required]
        public IssuingAuthority IssuingAuthority { get; set; }
        [Required]
        public Visitor Visitor { get; set; }
        public int VisitorId { get; set; }
    }
}
