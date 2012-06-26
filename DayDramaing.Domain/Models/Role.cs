using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayDramaing.Domain.Models
{
    public class Role
    {
        public virtual int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public Role()
        {
            Permissions = new List<Permission>();
        }
    }
}
