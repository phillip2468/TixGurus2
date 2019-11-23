using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ssample.Helpers.Authorisation
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string email, string[] roles)
        {
            Name = name;
            Email = email;
            Roles = roles;
        }

        public string Name { get; }
        public string Email { get; }
        public string[] Roles { get; }

        #region IIdentity Members
        public string AuthenticationType
        {
            get { return "Custom authentication"; }
        }
        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }
        #endregion

    }
}
