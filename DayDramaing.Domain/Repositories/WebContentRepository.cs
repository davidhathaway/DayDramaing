using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innovations.Core.Patterns;
using DayDramaing.Domain.Models;
using System.Security.Principal;

namespace DayDramaing.Domain.Repositories
{
    public interface IWebContentRepository : IRepository<WebContent>
    { 
    
    }

    public class WebContentRepository : DayDramaingRepository<WebContent>
    {
        public WebContentRepository(IPrincipal user)
            : base(user)
        {

        }
    }
}
