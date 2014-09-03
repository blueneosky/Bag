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

        private UserDomainInfo GetUserDomainInfo(string account)
        {
            UserDomainInfo info = null;
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
            finally
            {
                if (info == null)
                {
                    // fallback
                    info = new UserDomainInfo
                    {
                        FirstName = account.Split('/', '\\').LastOrDefault(),
                        Email = String.Empty,
                    };
                }
            }

            return info;
        }

        public class UserDomainInfo
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string ComposedName
            {
                get
                {
                    bool noFirstName = String.IsNullOrEmpty(FirstName);
                    bool noLastName = String.IsNullOrEmpty(LastName);

                    StringBuilder sb = new StringBuilder();
                    if (noFirstName && noLastName)
                        return "anonymous";
                    if (noLastName)
                        return FirstName;
                    if (noFirstName)
                        return LastName;

                    return String.Concat(FirstName, " ", LastName);
                }
            }
        }
    }
}