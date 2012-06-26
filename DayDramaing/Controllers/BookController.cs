using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayDramaing.Models;
using DayDramaing.Common;
using System.Net.Mail;
using System.Text;
using DayDramaing.Domain.Models;
using DayDramaing.Service.Services;
using Innovations.Core.Patterns;

namespace DayDramaing.Controllers
{
    public class BookController : BaseController<Booking, IBookingService>
    {
        //
        // GET: /Book/
        public ActionResult Index()
        {
            return View(new CreateBookModel());
        }

        [CaptchaValidatorAttribute]
        [HttpPost]
        public ActionResult Index(CreateBookModel model)
        {

 

            if (ModelState.IsValid)
            {
                try
                {
                    var result = Service.Create(new Booking()
                    {
                        CreatedOn = DateTime.Now,
                        Email = model.Email,
                        Name = model.Name,
                        NameOfSchool = model.NameOfSchool,
                        Telephone = model.Telephone,
                        What = model.What,
                        When = model.When,
                        YourLocation = model.YourLocation
                    });

                    if (result.Success)
                    {
                        model.BookingId = result.Entity.BookingId;

                        Dictionary<Func<CreateBookModel, string>, string> properties = new Dictionary<Func<CreateBookModel, string>, string>();

                        properties.Add((x) => x.BookingId.ToString(), "Id");
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
                    else
                    {
                        if (result.Exception != null)
                        {
                            ModelState.AddModelError("Error:", "There was an error saving your booking details : " + result.Exception);
                        }
                    }
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

        [Authorize]
        public ActionResult List()
        {
            var pageModel = new ListBookModel();
            pageModel.ResultCount = Service.FindAll().Count();
            pageModel.PageSize = pageModel.ResultCount; //will edit at a later date.
            pageModel.PageIndex = 0;

            var model = GetResults(pageModel);

            return View(model);
        }

        private List<ListBookModel> GetResults(IPageModel model)
        {
            return Service
               .FindAll()
               .OrderBy(x=>x.BookingId)
               .Skip(model.PageIndex * model.PageSize)
               .Take(model.PageSize)
               .Select(x => new ListBookModel()
                        {
                            PageIndex = model.PageIndex,
                            PageSize = model.PageSize,
                            ResultCount = model.ResultCount,
                            BookingId = x.BookingId,
                            CreatedOn = x.CreatedOn,
                            Email = x.Email,
                            Name = x.Name,
                            Telephone = x.Telephone
                        }).ToList();
        }

        protected override IBookingService CreateService()
        {
            return new BookingService(this.ModelStateWrapper);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var entity = Service.FindById(id);
            var model = new DisplayBookModel() 
            {
                BookingId = entity.BookingId, 
                CreatedOn = entity.CreatedOn,
                Email = entity.Email,
                Name = entity.Name,
                NameOfSchool = entity.NameOfSchool,
                Telephone = entity.Telephone,
                What = entity.What,
                When = entity.When,
                YourLocation = entity.YourLocation
            };
            return View(model);
        }
    }
}
