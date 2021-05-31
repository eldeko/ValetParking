using ValetParking.Dto.Enums;

namespace ValetParking.Dto
{
    public class VehicleDto
    {      
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public bool IsActive { get; set; }
    }
}
