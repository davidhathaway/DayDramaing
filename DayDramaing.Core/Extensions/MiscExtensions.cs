using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using Innovations.Core.Patterns;
using System.Web.Security;

namespace Innovations.Core.Extensions
{
    public static class MiscExtensions
    {
        public static void AddError<TEntity>(this IModelState errors, Expression<Func<TEntity, object>> selector, string errorMessage)
        {
            var propertyName = selector.GetInputName();
            errors.AddError(propertyName, errorMessage);
        }
        public static string GetUsername(this IPrincipal user)
        {
            if (user != null &&
                user.Identity != null &&
                !string.IsNullOrEmpty(user.Identity.Name))
            {
                return user.Identity.Name;
            }
            else
            {
                return null;
            }
        }
    }

}

