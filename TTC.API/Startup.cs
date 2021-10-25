using HyperV.BL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTC.BL;

namespace TTC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //        .AddJwtBearer(options =>
            //        {
            //            options.RequireHttpsMetadata = false;
            //            options.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                // ��������, ����� �� �������������� �������� ��� ��������� ������
            //                ValidateIssuer = false,
            //                // ������, �������������� ��������
            //                ValidIssuer = AuthOptions.ISSUER,

            //                // ����� �� �������������� ����������� ������
            //                ValidateAudience = false,
            //                // ��������� ����������� ������
            //                ValidAudience = AuthOptions.AUDIENCE,
            //                // ����� �� �������������� ����� �������������
            //                ValidateLifetime = true,

            //                // ��������� ����� ������������
            //                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //                // ��������� ����� ������������
            //                ValidateIssuerSigningKey = true,
            //            };
            //        });
            services.AddControllers();
            services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddScoped<IVmService, VmService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
