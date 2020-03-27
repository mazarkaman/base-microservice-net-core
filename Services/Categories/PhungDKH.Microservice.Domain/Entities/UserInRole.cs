using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhungDKH.Microservice.Domain.Entities
{
    [Table("UserInRole")]
    public class UserInRole : BaseEntity
    {
        public UserInRole() : base()
        {

        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
