using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayDramaing.Models;
using DayDramaing.Common;
using System.Net.Mail;
using DayDramaing.Service.Services;
using DayDramaing.Domain.Models;

namespace DayDramaing.Controllers
{
    public class HomeController : BaseController<WebContent, IWebContentService>
    {
        protected override IWebContentService CreateService()
        {
            return new WebContentService(ModelStateWrapper);
        }

        public ActionResult Index()
        {
            var model = new HomeModel();
            try
            {

                var intro = Service.FindFirst(x => x.Name == "Home Intro");
                var pageTitle = Service.FindFirst(x => x.Name == "Home Title");

                if (intro != null && pageTitle!=null)
                {
                    model.Intro = intro.RawHTML;
                    model.PageTitle = pageTitle.RawHTML;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Error";
                ViewBag.Error = ex;
            }

            return View(model);
        }

        public ActionResult About()
        {
            var aboutArea = Service.FindFirst(x => x.Name == "About Area");
            if (aboutArea != null)
            {
                ViewBag.AboutArea = aboutArea.RawHTML;
            }
            
            return View();
        }

        public ActionResult Contact()
        {
            var contactArea = Service.FindFirst(x => x.Name == "Contact Area");
            if (contactArea != null)
            {
                ViewBag.ContactArea = contactArea.RawHTML;
            }

            return View(new ContactModel());
        }

        [CaptchaValidatorAttribute]
        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = string.Format("Enquiry from Contact form is as follows:{0}{0}Name: {1}{0}Email : {2}{0}Telephone: {3}{0}Details: {0}{0}{4}", Environment.NewLine, model.Name, model.Email, model.Telephone, model.Enquiry);

                    //send msg
                    EmailHelper.SendContactEmail(body);
        
                    return RedirectToAction("ContactSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error:", "There was an error sending your contact details : " + ex.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult ContactSuccess()
        {
            return View();
        }

     
    }
}
