using HappyCows.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HappyCows.Models
{
    public class Calf : BaseEntity
    {
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Krava")]
        public Guid CowId { get; set; }

        public virtual Cow Cow { get; set; }

        public Calf()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}