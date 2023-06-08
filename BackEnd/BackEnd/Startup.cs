using BackEnd.Data;
using BackEnd.Iservices;
using BackEnd.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
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
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44328",
                    ValidAudience = "https://localhost:44328",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minimumSixteenCharacters"))
                };
            });
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ICloseAreaRepository, CloseAreaRepository>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IBookingByActivityRepository, BookingByActivityRepository>();
            services.AddTransient<IGuestActivityRepository, GuestActivityRepository>();
            services.AddTransient<IGuestTicketRepository, GuestTicketRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IGuestRepository, GuestRepository>();
            services.AddTransient<IGuestAreaRepository, GuestAreaRepository>();
            services.AddTransient<IWhereYouFromRepository, WhereYouFromRepository>();
            services.AddTransient<IHowYouKnowUsRepository, HowYouKnowUsRepository>();
            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

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
