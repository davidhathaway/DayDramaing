using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public static ServiceResult Run(Action action)
        {
            var result = new ServiceResult();
            result.Success = false;
            try
            {
                action();
                result.Success = true;
            }
            catch (Exception ex)
            {
                //if (HttpContext.Current != null)
                //{
                //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //}
                result.Exception = ex;
            }
            return result;
        }
    }
}
