using ValetParking.Persistence.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValetParking.Persistence.Entities
{ 
    [Table("Vehicles")]
    public class VehicleEntity : BaseEntity
        {         
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public bool IsActive { get; set; }
    }
}
