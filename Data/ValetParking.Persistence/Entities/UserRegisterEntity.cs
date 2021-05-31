using ValetParking.Dto;
using ValetParking.Persistence.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValetParking.Persistence.Entities
{
    public class UserRegisterEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool HasDisability { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public PinTypes PinType { get; set; }
        public string Pin { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public UserTypes UserType { get; set; }
        public List<VehicleDto> Vehicles { get; set; }
    }
}
