using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Innovations.Core.Patterns;
using Innovations.Core.Extensions;
using DayDramaing.Service;

namespace DayDramaing.Controllers
{
    public abstract class BaseController<TEntity, TService> : Controller
        where TService : IService<TEntity>
        where TEntity : class
    {

        public TService Service { get; set; }

        public IModelState ModelStateWrapper { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //get user
            ModelStateWrapper = new ModelStateWrapper(this);
            Service = CreateService();
      
        }

        protected abstract TService CreateService();
    }
}
