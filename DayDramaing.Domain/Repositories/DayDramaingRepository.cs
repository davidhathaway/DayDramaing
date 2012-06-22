using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innovations.Core.Patterns;
using DayDramaing.Domain.Models;
using System.Security.Principal;

namespace DayDramaing.Domain.Repositories
{
    public class DayDramaingRepository<T> : CodeFirstRepository<T, DayDramaingDBContext>
        where T: class
    {
        public DayDramaingRepository(IPrincipal user) : base(user)
        {
            
        }
    }
}
