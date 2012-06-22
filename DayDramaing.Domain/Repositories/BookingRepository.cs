using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innovations.Core.Patterns;
using DayDramaing.Domain.Models;
using System.Security.Principal;

namespace DayDramaing.Domain.Repositories
{

    public interface IBookingRepository : IRepository<Booking>
    { }

    public class BookingRepository : DayDramaingRepository<Booking>
    {
        public BookingRepository(IPrincipal user) : base(user)
        {

        }
    }
}
