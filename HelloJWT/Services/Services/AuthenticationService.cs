using DataAccsess;
using HelloJWT.Models;
using HelloJWT.Models.RequestModel;
using HelloJWT.Options;
using HelloJWT.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HelloJWT.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository<TokenModel, string> genericRepository;
        private readonly ILogger<AuthenticationService> logger;
        private readonly AuthenticationOptions options;
        private readonly UserOptions userOptions;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IOptions<AuthenticationOptions> options,
            IOptions<UserOptions> userOptions,
            IGenericRepository<TokenModel, string> genericRepository)
        {
            this.logger = logger;
            this.options = options.Value;
            this.userOptions = userOptions.Value;
            this.genericRepository = genericRepository;
        }

        public string GenerateToken(UserRequest user)
        {

            if (IsValidUserAndPassword(user))
            {
                User newUser = new User(user.UserName, user.Password);

                var someClaims = new Claim[]{

                    new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email,newUser.Email),
                     new Claim(JwtRegisteredClaimNames.NameId,newUser.UserId),
                    new Claim("FirstName",newUser.FirstName),
                    new Claim("LastName",newUser.LastName),
                };

                var token = CreateJwtBearer(someClaims, newUser);

                this.logger.LogInformation("JWT Token Created");

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            this.logger.LogError("UserName Or Password Wrongly Entered");

            throw new Exception("UserName And Password Not Valid");

        }

        private JwtSecurityToken CreateJwtBearer(Claim[] someClaims,User user)
        {

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecurityKey));
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: this.options.Issuer,
                audience: this.options.Audience,
                claims: someClaims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            this.genericRepository.Add(new TokenModel()
            {
                TokenContent = Fill_Payload(token.Payload),
                TokenCreateDate = DateTime.UtcNow,
                TokenEndDate = token.ValidTo,
                IP = GetIPAddress(),
                UserInfo = user
            }) ;

            return token;
        }

        private string GetIPAddress()
        {
            string IPAddress= string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }


        private string Fill_Payload(JwtPayload payload)
        {
            string payloadString = "{";

            foreach (var item in payload.Keys)
            {
                if (payload.TryGetValue(item, out object paloadValue))
                {
                    payloadString += item.ToString() + ":" + paloadValue.ToString() + " - ";
                }
            }
            payloadString +="}";

            return payloadString;
        }

        private bool IsValidUserAndPassword(UserRequest user)
        {
            if (this.userOptions.UserName == user.UserName 
                && this.userOptions.Password == user.Password)
            {
                return true;

            }

            return false;
        }

    }
}
