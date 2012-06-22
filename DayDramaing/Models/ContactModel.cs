using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DayDramaing.Models
{
    public class ContactModel
    {
        [Display(Name="Your name*")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "This is a required field*")]
        public string Name { get; set; }

        [Display(Name = "Your email*")]
        [Required(ErrorMessage = "This is a required field*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Telephone number")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Enquiry*")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "This is a required field*")]
        public string Enquiry { get; set; }

        [Display(Name = "Verify your a human*")]
        public bool CaptchaValid { get; set; }
    }
}