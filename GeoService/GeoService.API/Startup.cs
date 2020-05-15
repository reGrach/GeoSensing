using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Extension;
using GeoService.API.Auth;
using GeoService.API.Middleware;
using GeoService.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using GeoService.API.Auth.JwtExtension;
using Microsoft.AspNetCore.HttpOverrides;

namespace GeoService.API
{
    public class Startup
    {
        private const string _signingSecurityKey = "123456seckretkey7890";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddDbContext<GeoContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("GeoContext")));

            #region Создаем узел Аутентификации
            var section = Configuration.GetSection("AuthOptions");

            var options = section.Get<AuthOptions>();

            var jwtOptions = new JwtOptions(options.AUDIENCE, options.ISSUER, options.LIFETIME, _signingSecurityKey);

            services.AddApiJwtAuthentication(jwtOptions);

            services.Configure<AuthOptions>(section);
            #endregion

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseMiddleware<SecureJwtMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
