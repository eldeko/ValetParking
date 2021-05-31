using System;

namespace ValetParking.Models.Domain
    {
    public class ReservationDomain
        {
        public Guid ReservationId { get; set; }
        public Guid ParkingSlotId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        }
    }
