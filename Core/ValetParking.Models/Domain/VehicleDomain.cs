using ValetParking.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Models.Domain
{
   public class VehicleDomain
    {       
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public bool IsActive { get; set; }
    }
}
