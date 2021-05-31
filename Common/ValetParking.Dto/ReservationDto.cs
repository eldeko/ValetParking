using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Dto
{
    public class ReservationDto
    {
        public Guid ReservationId { get; set; }
        public Guid ParkingSlotId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
