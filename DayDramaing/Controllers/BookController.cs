using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayDramaing.Models;
using DayDramaing.Common;
using System.Net.Mail;
using System.Text;

namespace DayDramaing.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Book/
        public ActionResult Index()
        {
            return View(new BookModel());
        }

        //[CaptchaValidatorAttribute]
        [HttpPost]
        public ActionResult Index(BookModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Dictionary<Func<BookModel, string>, string> properties = new Dictionary<Func<BookModel, string>, string>();
                    properties.Add((x) => x.Name, "Name");
                    properties.Add((x) => x.Email, "Email");
                    properties.Add((x) => x.Telephone, "Telephone");
                    properties.Add((x) => x.NameOfSchool, "Name Of School");
                    properties.Add((x) => x.YourLocation, "Location");
                    properties.Add((x) => x.When, "When they want it");
                    properties.Add((x) => x.What, "What they want");
                  
                    var sb = new StringBuilder();
                    sb.AppendLine("Booking from Booking form is as follows:");
                    sb.AppendLine();
                    foreach (var item in properties)
                    {
                        sb.AppendFormat("{0} : {1}", item.Value, item.Key.Invoke(model));
                        sb.AppendLine();
                        sb.AppendLine();
                    }

                    var body = sb.ToString();

                    EmailHelper.SendBookingEmail(body);

                    return RedirectToAction("BookingSuccess");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error:", "There was an error sending your booking details : " + ex.Message);
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult BookingSuccess() 
        {
            return View();
        }

        
    }
}
