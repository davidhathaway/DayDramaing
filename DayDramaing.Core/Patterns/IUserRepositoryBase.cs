using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Innovations.Core.Patterns
{
    public interface IUserRepositoryBase : IDisposable
    {
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool ValidateUser(string username, string password);
        void UpdateUser(MembershipUser user);
        bool UnlockUser(string userName);
        string PasswordStrengthRegularExpression { get; }
        int MinRequiredPasswordLength { get; }
        int MinRequiredNonAlphanumericCharacters { get; }
        int MaxInvalidPasswordAttempts { get; }
        string GetUserNameByEmail(string email);
        MembershipUser GetUser(string providorName, string username, bool userIsOnline);
        int GetNumberOfUsersOnline();
        MembershipUserCollection GetAllUsers(string providorName, int pageIndex, int pageSize, out int totalRecords);
        MembershipUserCollection FindUsersByName(string providorName, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);
        MembershipUserCollection FindUsersByEmail(string providorName, string emailToMatch, int pageIndex, int pageSize, out int totalRecords);
        bool IsUserInRole(string username, string roleName);
    }
}
