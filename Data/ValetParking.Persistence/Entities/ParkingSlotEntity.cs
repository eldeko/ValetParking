using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValetParking.Persistence.Entities
{
    [Table("ParkingSlots")]
    public class ParkingSlotEntity : BaseEntity
    {
        [Required]
        public int Floor { get; set; }
        [Required]
        public string Sector { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool ForDisability { get; set; }
        public virtual List<ReservationEntity> Reservations { get; }
    }
}