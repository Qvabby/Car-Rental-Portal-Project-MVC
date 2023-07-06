using Car_Rental_Portal_Project_MVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Car_Rental_Portal_Project_MVC.Services.Implementations;

namespace Car_Rental_Portal_Project_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //------------ Services ------------------//

            //Mapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            //Adding DbContext To Application.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>

                options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalConnection"))

            );


            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "193979039961-k4pkpocougkrrb5llu9sovd3fdh612c2.apps.googleusercontent.com";
                options.ClientSecret = "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";
            }
               );



            //Adding Identity To Application.
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //Configuring Identity Options to Application.
            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = true;
                opt.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 0, 5);
                opt.Lockout.MaxFailedAccessAttempts = 2;
            });
            //Configuring Cookies To Application.
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied";
            });

            #region ForClaimsRoles
            //---------------------------------------------  FOR Claims/Roles  -----------------------------------------------------------

            //var authhelper = new HelperMethods();
            //builder.Services.AddAuthorization(opt =>
            //{
            //    opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            //    opt.AddPolicy("UserAndAdmin", policy => policy.RequireRole("Admin").RequireRole("User"));
            //    opt.AddPolicy("AdminCreate", policy => policy.RequireRole("Admin").RequireClaim("Create", "True"));
            //    opt.AddPolicy("AdminCreateDeleteEdit", policy => policy.RequireRole("Admin").RequireClaim("Create", "True").RequireClaim("Delete", "True").RequireClaim("Edit", "True"));

            //    opt.AddPolicy("AdminAllOrSuper",
            //        policy => policy.RequireAssertion(
            //            context =>
            //            (
            //            //method from helper
            //            authhelper.AuthorizeAdminWithClaimsOrSuperAdmin(context)
            //            )
            //        )
            //    );
            //    opt.AddPolicy("OnlySuperAdminChecker", policy => policy.Requirements.Add(new OnlySuperAdminChecker()));
            //    opt.AddPolicy("AdminWithMoreThan1000Days", policy => policy.Requirements.Add(new AdminWithMoreThan1000DaysRequirement(1000)));
            //    opt.AddPolicy("FirstNameAuth", policy => policy.Requirements.Add(new FirstNameAuthRequirement("Qvabby")));
            //});
            #endregion

            #region ForExternalLogin
            //---------------------------------------------------FOR EXTERNAL LOGIN--------------------------------------------------------

            //builder.Services.AddAuthentication()
            //    .AddFacebook(options =>
            //    {
            //        options.AppId = "191550813732362";
            //        options.AppSecret = "3e8675c736f20e5f844a0525da2d5b56";
            //    }).AddGoogle(options =>
            //    {
            //        options.ClientId = "246640904791-qh188s3h1sblnp0pa0js31ov344npev9.apps.googleusercontent.com";
            //        options.ClientSecret = "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";
            //    });
            #endregion

            #region TransientNScopeds
            //---------------------------------------------------- TRANSIENT SCOPED SYNTAX -> --------------------------------------------
            // builder.Services.AddTransient<>();
            // builder.Services.AddScoped<>();
            //------------------------------------------------------------------------------------------------------------------------
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            #endregion

            //building App.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Running App.
            app.Run();
        }
    }
}