using HelloJWT.Models.RequestModel;
using HelloJWT.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelloJWT.Controllers
{
    [Route("api/token")]
    [AllowAnonymous()]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        [HttpPost("createJwtBearer")]
        public IActionResult GetToken([FromBody]UserRequest user)
        {
            var token = this.authenticationService.GenerateToken(user);

            return new JsonResult(token);

        }

    }



}
