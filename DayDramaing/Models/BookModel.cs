using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DayDramaing.Models
{
    public class BookModel
    {
        [Display(Name = "Your name*")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage="This is a required field*")]
        public string Name { get; set; }

        [Display(Name = "Your email*")]
        [Required(ErrorMessage="This is a required field*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Name of your school, company or organisation (if applicable)")]
        public string NameOfSchool { get; set; }

        [Display(Name = "Your location* (eg. your postal address or just your town/area)")]
        [Required(ErrorMessage = "This is a required field*")]
        public string YourLocation { get; set; }

        [Display(Name = "What do you want to book?*")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "This is a required field*")]
        public string What { get; set; }

        [Display(Name = "When do you want to book it for?*")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "This is a required field*")]
        public string When { get; set; }

        [Display(Name = "Verify your a human*")]
        public bool CaptchaValid { get; set; }
    }
}