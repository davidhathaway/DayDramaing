using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayDramaing.Models;
using DayDramaing.Common;
using System.Net.Mail;

namespace DayDramaing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.Title = "Day Drama-ing";
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
                    var from = model.Email;
                    var to = "info@daydrama-ing.co.uk";
                    var subject = "Contact Form:";
                    var body = string.Format("Enquiry:{0}Name: {1}{0}Telephone: {2}{0}Details: {3}", Environment.NewLine, model.Name, model.Telephone, model.Enquiry);

                    //send msg
                    var smtpClient = new SmtpClient();
                    var msg = new MailMessage(from, to, subject, body);
                    smtpClient.Send(msg);
                    return RedirectToAction("ContactSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error:", "There was an error sending your contact details.");
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
