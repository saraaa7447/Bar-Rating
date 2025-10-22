using BarRating;
using BarRating.Data;
using BarRating.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<BarAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        CultureInfo ukCulture = new CultureInfo("en-GB");
        CultureInfo.DefaultThreadCurrentCulture = ukCulture;
        CultureInfo.DefaultThreadCurrentUICulture = ukCulture;

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        //Creates AdminAdminov2
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BarAppUser>>();

            string email = "admin.adminov2@gmail.com";
            string progileName = "AdminAdminov32562";
            string firstname = "Admin2";
            string lastname = "Adminov2";
            string password = "1qaz@WSX3edc$RFV2";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new BarAppUser();
                user.UserName = email;
                user.Email = email;
                user.ProfileName = progileName;
                user.FirstName = firstname;
                user.LastName = lastname;
                user.EmailConfirmed = false;

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        //Creates IvanIvanov
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BarAppUser>>();

            string email = "ivan.ivanov@gmail.com";
            string progileName = "IvanIvanov4257";
            string firstname = "Ivan";
            string lastname = "Ivanov";
            string password = "1Q2w3E4r5T!@#";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new BarAppUser();
                user.UserName = email;
                user.Email = email;
                user.ProfileName = progileName;
                user.FirstName = firstname;
                user.LastName = lastname;
                user.EmailConfirmed = false;

                await userManager.CreateAsync(user, password);
            }
        }


        app.Run();
    }
}
