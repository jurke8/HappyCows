using HappyCows.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyCows.Models
{
    public class Cow : BaseEntity
    {
        public int MarkNumber { get; set; }
        public CowStateEnum State { get; set; }
        public Cow(string name)
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
            Name = name;
        }
    }
}