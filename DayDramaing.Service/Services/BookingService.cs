using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DayDramaing.Domain.Models;
using Innovations.Core.Patterns;
using DayDramaing.Domain.Repositories;

namespace DayDramaing.Service.Services
{
    public interface IBookingService : IService<Booking>
    {

    }
    public class BookingService : ServiceBase<Booking,BookingRepository>, IBookingService
    {
        public BookingService(IModelState modelState)
            : base(modelState)
        {

        }

        protected override BookingRepository CreateRepository()
        {
    
            return new BookingRepository(this.ModelState.User);
        }

        public override bool Validate(Booking entity)
        {
            return ModelState.IsValid;
        }
    }
}
