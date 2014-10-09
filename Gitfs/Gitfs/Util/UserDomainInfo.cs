using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitfs.Util
{
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

        public string GitAuthor
        {
            get
            {
                string result = String.Format("{0} <{1}>", ComposedName, Email)
                    .Replace("\"", "");
                return result;
            }
        }
    }
}
