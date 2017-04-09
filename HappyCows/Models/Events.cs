using HappyCows.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HappyCows.Models
{
    public class Event : BaseEntity
    {
        [Display(Name = "Tip")]
        public EventEnum EventType { get; set; }

        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Krava")]
        public Guid CowId { get; set; }

        public virtual Cow Cow { get; set; }

        [NotMapped]
        public EventEnum? TypeFilter { get; set; }

        public Event()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}