namespace ValetParking.Models.Domain
{
    public class ParkingSlotDomain
    {
        public int Floor { get; set; }
        public string Sector { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public bool ForDisability { get; set; }
        public bool IsReserved { get; set; }
    }
}
