using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innovations.Core.Patterns;
using System.Web.Security;

namespace Innovations.Core.Providers
{
    public abstract class InnoRoleProvidor : RoleProvider
    {
        public abstract IUserRepositoryBase GetRepository();

        public InnoRoleProvidor()
            : base()
        {

        }

        public override string ApplicationName { get; set; }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var repo = GetRepository())
            {
                return repo.IsUserInRole(username, roleName);
            }
        }

        #region Not Implemented
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
