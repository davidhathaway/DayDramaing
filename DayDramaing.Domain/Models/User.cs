using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayDramaing.Domain.Models
{
    public class User
    {
        public virtual int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastUpdatePassword { get; set; }
        public Role Role { get; set; }
    }
}
