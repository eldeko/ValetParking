using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ValetParking.Persistence.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { set; get; }
    }
}
