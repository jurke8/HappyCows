using System;
using System.ComponentModel.DataAnnotations;

namespace HappyCows.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        public bool Deleted { get; set; }

        [Display(Name = "Datum kreiranja")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Napomena")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
    }
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool Deleted { get; set; }
        DateTime DateCreated { get; set; }
        string Note { get; set; }
    }
}