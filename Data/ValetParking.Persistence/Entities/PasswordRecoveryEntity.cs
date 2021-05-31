using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValetParking.Persistence.Entities
{
    [Table("PasswordRecovery")]
    public class PasswordRecoveryEntity : BaseEntity
    {
        public virtual UserEntity User { get; set; }
        public Guid token { get; set; }
        public DateTime Date = DateTime.Now;
    }
}