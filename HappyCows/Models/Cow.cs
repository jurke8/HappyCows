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
        [DataType(DataType.Date)]
        [Display(Name = "Trajanje")]
        public DateTime DurationOfState { get; set; }

        [NotMapped]
        [Display(Name = "Prošli događaj")]
        public EventEnum PreviousEvent { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        [Display(Name = "Vrijeme prošlog događaja")]
        public DateTime DateOfPreviousEvent { get; set; }

        [Display(Name = "Djeca")]
        public virtual ICollection<Calf> Calfs { get; set; }


        public Cow()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}