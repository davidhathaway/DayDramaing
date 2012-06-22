using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{

    public abstract class PageModel : IPageModel
    {
        public int ResultCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int SortBy { get; set; }
        public bool SortDesc { get; set; }

        public PageModel()
        {
            PageSize = 10;
            PageIndex = 0;
            ResultCount = 0;
            SortBy = 0;
            SortDesc = false;
        }

        public PageModel(IPageModel model)
            : base()
        {
            if (model != null)
            {
                this.PageIndex = model.PageIndex;
                this.PageSize = model.PageSize;
                this.ResultCount = model.ResultCount;
                this.SortBy = model.SortBy;
                this.SortDesc = model.SortDesc;
            }
        }

        public string ShowingText
        {
            get
            {
                var from = this.PageSize * this.PageIndex;
                var to = Math.Min(this.ResultCount, from + this.PageSize);
                return string.Format("Showing {0}-{1} of {2} results", from, to, this.ResultCount);
            }
        }

        public List<int> PageSizes
        {
            get
            {
                return new List<int>() { 10, 25, 50, 100 };
            }
        }
    }

}
