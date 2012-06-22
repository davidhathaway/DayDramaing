using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DayDramaing.Domain.Models;
using Innovations.Core.Patterns;
using DayDramaing.Domain.Repositories;

namespace DayDramaing.Service.Services
{
    public interface IWebContentService : IService<WebContent>
    {

    }

    public class WebContentService : ServiceBase<WebContent, WebContentRepository>, IWebContentService
    {
        public WebContentService(IModelState modelState) : base(modelState)
        {

        }

        protected override WebContentRepository CreateRepository()
        {
            return new WebContentRepository(this.ModelState.User);
        }
        public override bool Validate(WebContent entity)
        {
            return ModelState.IsValid;
        }
    }
  
}
