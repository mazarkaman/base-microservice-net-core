using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhungDKH.Microservice.Domain.Entities
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        public Role() : base()
        {

        }

        public string Name { get; set; }
        public List<UserInRole> UserInRoles { get; set; }
    }
}
