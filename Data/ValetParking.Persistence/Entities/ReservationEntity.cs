using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ValetParking.Persistence.Entities
{
    [Table("Reservations")]
    public class ReservationEntity : BaseEntity
    {        
        public virtual ParkingSlotEntity ParkingSlot { get; set; }
        public virtual VehicleEntity Vehicle { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}