using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace Gitfs.Util
{
    internal class ActiveDirectoryHelper
    {
        private const string ConstPropertyFirstName = "givenName";
        private const string ConstPropertyLastName = "sn";
        private const string ConstPropertyEmail = "mail";

        private static Dictionary<string, UserDomainInfo> _userDomainInfoByUserAccount = new Dictionary<string, UserDomainInfo> { };

        public static UserDomainInfo GetUserDomainInfo(string userAccount)
        {
            UserDomainInfo info = GetUserDomainInfoCore(userAccount);

            if (info == null)
            {
                // fallback
                info = GetUserDomainInfoCore(Environment.UserName);

                string emailServer = info.Email.Split('@').LastOrDefault();
                if (String.IsNullOrEmpty(emailServer))
                    emailServer = "nop.com";

                string account = GetAccountFromUserAccount(userAccount);
                info.FirstName = account;
                info.LastName = null;
                info.Email = String.Concat(account, "@", emailServer);
            }

            _userDomainInfoByUserAccount[userAccount] = info;   // update cache

            return info;
        }

        private static UserDomainInfo GetUserDomainInfoCore(string userAccount)
        {
            string account = GetAccountFromUserAccount(userAccount);

            UserDomainInfo info = null;

            // search in cache
            if (_userDomainInfoByUserAccount.TryGetValue(userAccount, out info))
                return info;

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry())
                {
                    // get a DirectorySearcher object
                    DirectorySearcher search = new DirectorySearcher(entry);

                    // specify the search filter
                    search.Filter = "(&(objectClass=user)(anr=" + account + "))";

                    // specify which property values to return in the search
                    search.PropertiesToLoad.Add(ConstPropertyFirstName);    // first name
                    search.PropertiesToLoad.Add(ConstPropertyLastName);     // last name
                    search.PropertiesToLoad.Add(ConstPropertyEmail);        // smtp mail address

                    // perform the search
                    SearchResult result = search.FindOne();

                    info = new UserDomainInfo
                    {
                        FirstName = (string)result.Properties[ConstPropertyFirstName][0],
                        LastName = (string)result.Properties[ConstPropertyLastName][0],
                        Email = (string)result.Properties[ConstPropertyEmail][0],
                    };
                }
            }
            catch (Exception) { }

            return info;
        }

        private static string GetAccountFromUserAccount(string userAccount)
        {
            string account = userAccount.Split('/', '\\').LastOrDefault();
            return account;
        }
    }
}