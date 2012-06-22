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

            try
            {
                var any = Service.FindAll().ToList();

                if (any.Count > 0)
                {
                    ViewBag.Title = "Day Drama-ing";
                }
            }
            catch (Exception ex)
            {
               
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
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
