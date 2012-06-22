using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public interface IPageModel
    {
        int ResultCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int SortBy { get; set; }
        bool SortDesc { get; set; }
    }
}
