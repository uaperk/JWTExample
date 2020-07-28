using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloJWT.Models.RequestModel
{
    public class UserRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
