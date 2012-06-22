using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayDramaing.Domain.Models
{
    public class WebContent
    {
        public virtual int WebContentId { get; set; }
        public string Name { get; set; }
        public string RawHTML { get; set; }
    }
}
