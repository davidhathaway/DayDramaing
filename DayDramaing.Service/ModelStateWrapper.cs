using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Innovations.Core.Patterns;
using Innovations.Core.Extensions;
using System.Security.Principal;
namespace DayDramaing.Service
{
    public class ModelStateWrapper : IModelState
    {
        private ModelStateDictionary _modelState;

        public ModelStateWrapper(Controller controller)
        {
            _modelState = controller.ModelState;
            this.User = controller.User;
            this.Username = controller.User.GetUsername();
        }

        #region IModelState Members

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get { return _modelState.IsValid; }
        }

        public string Username { get; set; }

        #endregion
        public IPrincipal User { get; set; }
    }
}
