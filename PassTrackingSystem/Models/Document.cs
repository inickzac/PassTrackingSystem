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
        [Required, DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        public  DocumentType DocumentType { get; set; }
        [Required]
        public int IssuingAuthorityId { get; set; }
        public IssuingAuthority IssuingAuthority { get; set; }
        [Required]
        public Visitor Visitor { get; set; }
        public int VisitorId { get; set; }
    }
}
