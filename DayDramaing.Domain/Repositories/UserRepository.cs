using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using DayDramaing.Domain.Models;
using Innovations.Core.Patterns;
using Innovations.Core.Security;

namespace DayDramaing.Domain.Repositories
{
    public interface IUserRepository :IRepository<User>, IUserRepositoryBase
    {

    }
    public class UserRepository : DayDramaingRepository<User>, IUserRepository
    {
        public UserRepository():this(null)
        {

        }
        public UserRepository(IPrincipal user) : base(user)
        {

        }
       
        public User FindByUsername(string username)
        {
            return this.FindAll(x => x.Username == username).FirstOrDefault();
        }
        
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {

                try
                {
                    var user = FindByUsername(username);
                    user.Password = PasswordHash.HashPassword(newPassword);
                    Save();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                var user = FindByUsername(username);
                if (user == null) { return false; }
                return PasswordHash.ValidatePassword(password, user.Password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public int MinRequiredPasswordLength
        {
            get { return 8; }
        }

        public int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public string GetUserNameByEmail(string email)
        {
            var user = FindAll(x => x.Email == email).FirstOrDefault();

            if (user != null)
            {
                return user.Username;
            }
            else
            {
                return null;
            }
        }

        public System.Web.Security.MembershipUser GetUser(string providorName, string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public int GetNumberOfUsersOnline()
        {
            return 0;
        }
        public System.Web.Security.MembershipUserCollection GetAllUsers(string providorName, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public System.Web.Security.MembershipUserCollection FindUsersByName(string providorName, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public System.Web.Security.MembershipUserCollection FindUsersByEmail(string providorName, string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return FindAll(x => x.Username == username && x.Role.RoleName == roleName).Any();
        }
    }
}
