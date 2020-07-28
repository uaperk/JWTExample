using DataAccsess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloJWT.Models
{
    public class TokenModel : IEntity<string>
    {
        public string Id { get; set; }

        public string TokenContent { get; set; }

        public DateTime TokenCreateDate { get; set; }

        public DateTime TokenEndDate { get; set; }

        public string IP { get; set; }

        public User UserInfo { get; set; }
    }
}
