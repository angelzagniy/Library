using System;
using Library.Web.BusinessLogic.Abstract;
using Library.Web.BusinessLogic.Managers;
using Library.Web.BusinessLogic.Repository;
using Library.Web.BusinessLogic.Repository.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Library.Web;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(name: "LibraryDatabase")))
            .AddTransient<IBooksVMBuilder, BooksVmBuilder>()
            .AddTransient<IBooksRepository, BooksRepository>()
            .AddTransient<IAuthorsRepository, AuthorsRepository>()
            .AddTransient<IMembersRepository, MembersRepository>()
            .AddTransient<IUserRepository, UserRepository>();

        // Add the localization services to the services container
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        
        builder.Services.AddControllersWithViews() 
            // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            // Add support for localizing strings in data annotations (e.g. validation messages) via the
            // IStringLocalizer abstractions.
            .AddDataAnnotationsLocalization();

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Home/Login";
                options.AccessDeniedPath = "/Home/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(0, 0, minutes: 30, 0);
            });

       builder.Services.Configure<RequestLocalizationOptions>(
            options =>
            {
                string[] supportedCultures = ["en-US", "uk-UA"];
                
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
                
                // RequestLocalizationOptions has three default request culture providers:
                //   QueryStringRequestCultureProvider,
                //   CookieRequestCultureProvider,
                //   and AcceptLanguageHeaderRequestCultureProvider.
                // Here we want to retain only CookieRequestCultureProvider
                options.RequestCultureProviders.RemoveAt(2);
                options.RequestCultureProviders.RemoveAt(0);
            });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        }

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days.
            // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(locOptions.Value);
        
        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        CreateDbIfNotExists(app);

        app.Run();
    }

    private static void CreateDbIfNotExists(IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();

        IServiceProvider services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<LibraryContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database");
        }
    }
}