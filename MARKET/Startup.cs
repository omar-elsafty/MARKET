using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MARKET.Data.ModelsRepo.Interfaces;
using MARKET.Data.Repository;
using MARKET.Data.UnitOfWork;
using MARKET.Models;
using MARKET.Services;
using MARKET.Services.Comunications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MARKET
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
            //Add data base
            services.AddDbContextPool<DBMarket>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MarketDB")));

            //Add auto mapper
            services.AddAutoMapper(typeof(Startup));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Add configuration for Identity
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DBMarket>()
             .AddDefaultTokenProviders();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "auth_cookie";
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.LoginPath = new PathString("/api/auth/login");
            //    options.AccessDeniedPath = new PathString("/api/auth/login");

            //    // Not creating a new object since ASP.NET Identity has created
            //    // one already and hooked to the OnValidatePrincipal event.
            //    // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //});

            //Add configuration for jwt for authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        //in appsetting.json file ["Jwt:Issuer"]
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            //add dependancy injection for user services class to use it (Authentication class)

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentTypeRepository,PaymentTypeRepository>();

            services.AddControllers();
            services.AddRazorPages();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
