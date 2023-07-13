using BackEnd.Data;
using BackEnd.Helpers;
using BackEnd.Iservices;
using BackEnd.service;
using BackEnd.Validations;
using DocumentFormat.OpenXml.EMMA;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]))
                };
            });
            services.AddControllers();
            //.AddFluentValidation(c =>
            //c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationFilter());
            }).AddFluentValidation(option =>
            {
                option.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
            });
            services.AddAuthorizationCore(
              options =>
              {
                  options.AddPolicy("Booking_Show",
                      policy => policy.RequireClaim("Booking_Show", "true"));
                  options.AddPolicy("Booking_Add",
                      policy => policy.RequireClaim("Booking_Add", "true"));
                  options.AddPolicy("Booking_Edit",
                      policy => policy.RequireClaim("Booking_Edit", "true"));
                  options.AddPolicy("Booking_Delete",
                      policy => policy.RequireClaim("Booking_Delete", "true"));
              });
          //  services.AddSwaggerGen();
            //services.ConfigureSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v3", new OpenApiInfo
            //    {
            //        Title = "GTrackAPI",
            //        Version = "v3"
            //    });
            //});
            services.AddMvc(x => x.Conventions.Add(new ApiExplorerVersionConvention()));
            services.AddCors().AddAuthorization(); // Note - this is on the IMvcBuilder, not the service collection;
          
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
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IGuestEventRepository, GuestEventRepository>();
            services.AddTransient<IDepositWayesRepository, DepositWayRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //    c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
             
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseDeveloperExceptionPage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
