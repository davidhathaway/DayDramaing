using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayDramaing.Domain.Models;
using DayDramaing.Service.Services;
using DayDramaing.Models;
using Innovations.Core.Patterns;

namespace DayDramaing.Controllers
{
    public class WebContentController : BaseController<WebContent, IWebContentService>
    {
        [Authorize]
        public ActionResult List()
        {
            var pageModel = new WebContentResultsModel();
            pageModel.ResultCount = Service.FindAll().Count();
            pageModel.PageSize = 25;
            pageModel.PageIndex = 0;

            var model = GetResults(pageModel);

            return View(model);
        }

        private List<WebContentResultsModel> GetResults(IPageModel model)
        {
            return Service
               .FindAll()
               .OrderBy(x=>x.WebContentId)
               .Skip(model.PageIndex * model.PageSize)
               .Take(model.PageSize)
               .Select(x => new WebContentResultsModel()
               {
                   PageIndex = model.PageIndex,
                   PageSize = model.PageSize,
                   ResultCount = model.ResultCount,
                   WebContentId = x.WebContentId,
                   Name = x.Name
               }).ToList();
        }

        protected override IWebContentService CreateService()
        {
            return new WebContentService(this.ModelStateWrapper);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var entity = Service.FindById(id);
            var model = new WebContentModel() 
            { 
                WebContentId = entity.WebContentId,
                Content = entity.RawHTML,
                Name = entity.Name 
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(WebContentModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = Service.FindById(model.WebContentId);
                entity.RawHTML = model.Content;
                
                var result = Service.Update(entity);
                if (result.Success)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    if (result.Exception != null)
                    {
                        ModelState.AddModelError("Error:", "There was updating this Web Content : " + result.Exception);
                    }
                }
            }

            return View(model);
        }
    }
}
