using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public interface IModelState
    {
        IPrincipal User { get; set; }
        string Username { get; set; }
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
