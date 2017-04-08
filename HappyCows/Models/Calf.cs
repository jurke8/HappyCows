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
        public DateTime DateOfBirth { get; set; }

        public Guid MotherId { get; set; }

        [Display(Name = "Mama")]
        public virtual Cow Mother { get; set; }

        public Calf()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}