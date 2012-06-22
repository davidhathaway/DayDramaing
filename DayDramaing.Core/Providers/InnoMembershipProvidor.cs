using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Collections.Specialized;
using Innovations.Core.Patterns;

namespace Innovations.Core.Providers
{
    public abstract class InnoMembershipProvidor : MembershipProvider
    {
        public abstract IUserRepositoryBase GetRepository();

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            ApplicationName = config["applicationName"];

            //enablePasswordReset = Convert.ToBoolean(config["enablePasswordReset"]);
            //enablePasswordRetrieval = Convert.ToBoolean(config["enablePasswordRetrieval"]);
            //maxInvalidPasswordAttempts = Convert.ToInt32(config["maxInvalidPasswordAttempts"]);
            //minRequiredNonAlphanumericCharacters = Convert.ToInt32(config["minRequiredNonalphanumericCharacters"]);
            //minRequiredPasswordLength = Convert.ToInt32(config["minRequiredPasswordLength"]);
            //passwordAttemptWindow = Convert.ToInt32(config["passwordAttemptWindow"]);
            //requiresQuestionAndAnswer = Convert.ToBoolean(config["requiresQuestionAndAnswer"]);
            //requiresUniqueEmail = Convert.ToBoolean(config["requiresUniqueEmail"]);
        }

        public override string ApplicationName { get; set; }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var repo = GetRepository())
            {
                return repo.ChangePassword(username, oldPassword, newPassword);
            }
        }
        public override bool EnablePasswordReset
        {
            get { return true; }
        }
        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var repo = GetRepository())
            {
                int total = 0;
                var result = repo.FindUsersByEmail(this.Name, emailToMatch, pageIndex, pageSize, out total);
                totalRecords = total;
                return result;
            }
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var repo = GetRepository())
            {
                int total = 0;
                var result = repo.FindUsersByName(this.Name, usernameToMatch, pageIndex, pageSize, out totalRecords);
                totalRecords = total;
                return result;
            }
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            using (var repo = GetRepository())
            {
                int total = 0;
                var result = repo.GetAllUsers(this.Name, pageIndex, pageSize, out totalRecords);
                totalRecords = total;
                return result;
            }
        }
        public override int GetNumberOfUsersOnline()
        {
            using (var repo = GetRepository())
            {
                return repo.GetNumberOfUsersOnline();
            }
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var repo = GetRepository())
            {
                return repo.GetUser(this.Name, username, userIsOnline);
            }
        }
        public override string GetUserNameByEmail(string email)
        {
            using (var repo = GetRepository())
            {
                return repo.GetUserNameByEmail(email);
            }
        }
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                using (var repo = GetRepository())
                {
                    return repo.MaxInvalidPasswordAttempts;
                }
            }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                using (var repo = GetRepository())
                {
                    return repo.MinRequiredNonAlphanumericCharacters;
                }
            }
        }
        public override int MinRequiredPasswordLength
        {
            get
            {
                using (var repo = GetRepository())
                {
                    return repo.MinRequiredPasswordLength;
                }
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                using (var repo = GetRepository())
                {
                    return repo.PasswordStrengthRegularExpression;
                }
            }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }
        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }
        public override bool UnlockUser(string userName)
        {
            using (var repo = GetRepository())
            {
                return repo.UnlockUser(userName);
            }
        }
        public override void UpdateUser(MembershipUser user)
        {
            using (var repo = GetRepository())
            {
                repo.UpdateUser(user);
            }
        }
        public override bool ValidateUser(string username, string password)
        {
            using (var repo = GetRepository())
            {
                return repo.ValidateUser(username, password);
            }
        }


        #region Not implmented
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
