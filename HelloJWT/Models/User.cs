using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloJWT.Models
{
    public class User
    {

        public User(string userName, string password)
        {
            this.Password = password;
            this.UserName = userName;
        }

        public string UserName { get; set; }

        private string Password { get; set; }

        public string FirstName { get; set; } = "Uğur Andaç";

        public string LastName { get; set; } = "PERK";

        public string Email { get; set; } = "uaperk@gmail.com";

        public string UserId { get; set; } = Guid.NewGuid().ToString();


    }
}
