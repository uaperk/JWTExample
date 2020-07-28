using HelloJWT.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloJWT.Services.Interfaces
{
   public interface IAuthenticationService
    {
        string GenerateToken(UserRequest user);
    }
}
