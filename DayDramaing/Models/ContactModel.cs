using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DayDramaing.Models
{
    public class ContactModel
    {
        [Display(Name="Name")]
        [DataType(DataType.Text)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Enquiry")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Enquiry { get; set; }

        [Display(Name = "I'm not a robot")]
        public bool CaptchaValid { get; set; }
    }
}