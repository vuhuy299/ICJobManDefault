using ICSLib.Authen.Data.EF;
using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Models.Users.Validators;
using ICSLib.Authen.Services;
using ICSLib.Authen.Services.Interfaces;
using ICSLib.Utilities.Helpers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ICSLib.JobManApp
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
            // Đăng ký DBContext
            var dbType = Configuration.GetConnectionString(ConstantHelper.AuthenDBType).ToLower();
            if (dbType.Equals(ConstantHelper.SQLServer.ToLower()))
            {
                string connectionString = Configuration.GetConnectionString(ConstantHelper.AuthenSQLServer);
                services.AddDbContext<AuthenDbContext>(
                    options =>
                    {
                        options.UseSqlServer(connectionString);
                    }
                );
            }
            else if (dbType.Equals(ConstantHelper.MySQL.ToLower()))
            {
                string connectionString = Configuration.GetConnectionString(ConstantHelper.AuthenMySQL);
                services.AddDbContext<AuthenDbContext>(
                    options =>
                    {
                        options.UseMySql(connectionString,
                            ServerVersion.AutoDetect(connectionString),
                            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                                    maxRetryCount: 10,
                                    maxRetryDelay: TimeSpan.FromSeconds(30),
                                    errorNumbersToAdd: null
                            )
                        );
                    }
                );
            }
            // Kết thúc đăng ký DBContext

            // Đăng ký Identity
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuthenDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            // Kết thúc đăng ký Identity

            services.AddHttpClient();

            //Add dich vu Authen
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login/";
                    options.LogoutPath = "/account/logout/";
                    options.AccessDeniedPath = "/account/accessdenied/";
                });

            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });


            //Khai bao cac repositories
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserManager<User>, UserManager<User>>();
            services.AddTransient<SignInManager<User>, SignInManager<User>>();
            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleClaimService, RoleClaimService>();
            services.AddTransient<IUserClaimService, UserClaimService>();
            services.AddTransient<IRoleGroupService, RoleGroupService>();
            services.AddTransient<IGenderService, GenderService>();
            //Ket thuc Khai bao cac repositories


            //Cau hinh de su dung format datetime dd/MM/yyyy
            //Trong ham Configure can them dong: app.UseRequestLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("vi-VN");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("vi-VN") };
            });
            //Ket thuc format datetime dd/MM/yyyy


            //Cau hinh cho phep F5 de nhan thay doi tren razor code
            IMvcBuilder builder = services.AddRazorPages();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();
            }
            //Ket thuc cho phep F5 de nhan thay doi tren razor code
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Public/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRequestLocalization();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
