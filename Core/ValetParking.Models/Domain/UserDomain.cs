using ValetParking.Persistence.Enums;
using System;

namespace ValetParking.Models.Domain
{
    public class UserDomain
    {        
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public bool HasDisability { get; set; }
        public string Email { get; set; }
        public PinTypes PinType { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public UserTypes UserType { get; set; }
        public string Token { get; set; }
    }
}
