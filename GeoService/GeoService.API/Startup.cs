using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Auth;
using GeoService.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GeoService.API
{
    public class Startup
    {

        private string _signingSecurityKey = null;
        private string _connection = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            #region Создаем узел подключения к БД
            //"PasswordDb": "Password=geosensing2020",
            _connection = $"{Configuration.GetConnectionString("TestContext")}{Configuration["PasswordDb"]}";
            services.AddDbContext<GeoContext>(opt => opt.UseNpgsql(_connection));
            #endregion

            #region Создаем узел Аутентификации
            _signingSecurityKey = Configuration["ServiceApiKey"];
            var signingKey = new SigningSymmetricKey(_signingSecurityKey);
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = GetTokenParameters(signingDecodingKey.GetKey());
                });

            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    var token = context.Request.Cookies[".Core.Geo.Auth"];
            //    if (!string.IsNullOrEmpty(token))
            //        context.Request.Headers.Add("Authorization", "Bearer " + token);

            //    await next();
            //});

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private TokenValidationParameters GetTokenParameters(SecurityKey key) =>
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetSection("AuthOptions").GetValue<string>("ISSUER"),

                ValidateAudience = true,
                ValidAudience = Configuration.GetSection("AuthOptions").GetValue<string>("AUDIENCE"),

                ValidateLifetime = true, 

                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
            };
    }
}
