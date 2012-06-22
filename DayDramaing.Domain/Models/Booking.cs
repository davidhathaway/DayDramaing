using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DayDramaing.Domain.Models
{
    public class Booking
    {
        public virtual int BookingId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string NameOfSchool { get; set; }
        public string YourLocation { get; set; }
        public string What { get; set; }
        public string When { get; set; }
    }
}
