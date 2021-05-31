using ValetParking.Dto.Enums;

namespace ValetParking.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool HasDisability { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public PinTypes PinType { get; set; }
        public string Pin { get; set; }
        public UserTypes UserType { get; set; }
    }
}
