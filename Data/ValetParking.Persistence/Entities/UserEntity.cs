using ValetParking.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValetParking.Persistence.Entities
{
[Table("Users")] 
    public class UserEntity : BaseEntity
        {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public bool HasDisability { get; set; }       
        public string Email { get; set; }
        public PinTypes PinType { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public UserTypes UserType{ get; set; }
        public string Token { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public virtual List<VehicleEntity> Vehicles { get; set; }
        public string AvatarURL { get; set; }
    }
}
