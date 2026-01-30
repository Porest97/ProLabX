using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using ProLab.Areas.HockeyData.Data;
using ProLab.Areas.HockeyData.Services;
using ProLab.Areas.ProGym;
using ProLab.Areas.ProGym.Data;
using ProLab.Areas.XLab.Data;
using ProLab.Data;
using ProLab.Models.DataModels;

namespace ProLab
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // EPPlus 8.x – korrekt licens
            ExcelPackage.License.SetNonCommercialPersonal("Per Orest");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // ===============================
            // MVC + Razor
            // ===============================
            services.AddControllersWithViews();
            services.AddRazorPages();

            // ===============================
            // Upload limits
            // ===============================
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = long.MaxValue;
            });

            // ===============================
            // PostgreSQL DbContext
            // ===============================
            services.AddDbContext<ProLabContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("ProLabContext"),
                    npgsql =>
                    {
                        npgsql.EnableRetryOnFailure();
                    }
                )
            );            

            // ===============================
            // Identity (DIN ApplicationUser)
            // ===============================
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ProLabContext>()
            .AddDefaultTokenProviders();

            // ===============================
            // Global auth-policy
            // ===============================
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlDataContractSerializerFormatters();

            // ===============================
            // Claims / Policies
            // ===============================
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateRolePolicy", p => p.RequireClaim("Create Role"));
                options.AddPolicy("DeleteRolePolicy", p => p.RequireClaim("Delete Role"));
                options.AddPolicy("EditRolePolicy", p => p.RequireClaim("Edit Role"));
                options.AddPolicy("AdminRolePolicy", p => p.RequireClaim("Admin"));
            });

            services.AddDbContext<HockeyDataContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("HockeyDataContext")));

            services.AddScoped<HockeyDataStatsService>();

            services.AddDbContext<ProGymContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProGymContext")));
            services.AddDbContext<XLabContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("XLabContext")));



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}