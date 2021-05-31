using ValetParking.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ValetParking.Dto
{
    public class LoggedUserDto
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }
       
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
        
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "avatarUrl")]
        public string AvatarURL { get; set; }

        [DataMember(Name = "userType")]
        public UserTypes UserType { get; set; }
    }
}
