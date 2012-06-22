using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public class EntityBase
    {
        public virtual int CreatedById { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual int? UpdatedById { get; set; }
        public virtual DateTime? UpdatedOn { get; set; }
        public virtual int Version { get; set; }
        public virtual bool Removed { get; set; }
    }
}
