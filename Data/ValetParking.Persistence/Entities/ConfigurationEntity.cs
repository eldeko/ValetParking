using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValetParking.Persistence.Entities
{
    [Table("Configurations")]
    public class ConfigurationEntity : BaseEntity
    {
        public string ConfigurationKey { get; set; }
        public string JsonData { get; set; }
    }
}
