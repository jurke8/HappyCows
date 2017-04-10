using HappyCows.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HappyCows.Models
{
    public class Cow : BaseEntity
    {
        [Display(Name = "Stanje")]
        public CowStateEnum State { get; set; }

        [NotMapped]
        [Display(Name = "Trajanje")]
        public int DurationOfState { get; set; }

        [Display(Name = "Vrijeme prošlog događaja")]
        public DateTime DateOfPreviousEvent { get; set; }

        [NotMapped]
        public CowStateEnum? StateFilter { get; set; }

        public Cow()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}