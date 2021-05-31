using ValetParking.Dto.Enums;

namespace ValetParking.Dto
{
    public class ParkingSlotDto
    {
        public int Floor { get; set; }
        public string Sector { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public bool ForDisability { get; set; }
        public StatusTypes Status { get; set; }
    }
}
