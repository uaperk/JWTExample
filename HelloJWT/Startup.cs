using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloJWT.Extensions;
using HelloJWT.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelloJWT
{
    public class Startup
    {
        private readonly string Audience = "uap_dev";

        private readonly string Issuer = "uap_dev_user";

        private readonly string SecretKey = "vrjzLb2umJuLL9WkcLRM1LHHdmrzuFFq";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
           )
                 .AddJwtBearer(options => {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateAudience = true,
                         ValidAudience = this.Audience,
                         ValidateIssuer = true,
                         ValidIssuer = this.Issuer,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(SecretKey))
                     };

                     options.Events = new JwtBearerEvents
                     {
                         OnTokenValidated = ctx => {
                             //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulam yapılabilir.

                             if (ctx.SecurityToken.ValidTo < DateTime.UtcNow)
                             {
                                 throw new Exception("Could not get exp claim from token");
                             }
                               
                             return Task.CompletedTask;
                         },
                         OnAuthenticationFailed = ctx => {
                             Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                             return Task.CompletedTask;
                         }
                     };
                 });


            services.AddAuthenticationServices(this.Configuration);

            services.AddDtoOption(this.Configuration);

            services.AddDbOptionManager(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.Use(async delegate (HttpContext context, Func<Task> next)
            {

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("<ConnectionInfo>");
                sb.AppendLine($"Remote_Ip_Adress:{ context.Connection.RemoteIpAddress.ToString()} ");
                    foreach (var header in context.Request.Headers)
                    {
                        sb.AppendLine($"Header : {header.Key} --- > {header.Value}");

                    }

                    Console.WriteLine(sb.ToString());

                    //logger.LogInformation(sb.ToString());

                }
                catch
                {
                }
                await next.Invoke();
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
