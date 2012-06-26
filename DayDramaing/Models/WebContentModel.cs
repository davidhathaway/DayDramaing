using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Innovations.Core.Patterns;

namespace DayDramaing.Models
{
    public class WebContentModel 
    {
        [Display(Name = "ID")]
        public int WebContentId { get; set; }

        [Display(Name = "Content")]
        [Required]
        [UIHint("tinymce_jquery_lite"), AllowHtml]
        public string Content { get; set; }

        [Display(Name="Name")]
        [Required]
        public string Name { get; set; }
    }


    public class WebContentResultsModel : PageModel
    {
        [Display(Name = "ID")]
        public int WebContentId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
    }
}
