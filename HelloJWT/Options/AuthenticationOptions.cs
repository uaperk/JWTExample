using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloJWT.Options
{
    public class AuthenticationOptions
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
