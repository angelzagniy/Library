using System;
using Library.Web.BusinessLogic.Abstract;
using Library.Web.BusinessLogic.Managers;
using Library.Web.BusinessLogic.Repository;
using Library.Web.BusinessLogic.Repository.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

		builder.Services.AddControllersWithViews();

		builder.Services
			.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = "/Home/Login";
				options.AccessDeniedPath = "/Home/AccessDenied";
				options.SlidingExpiration = true;
				options.ExpireTimeSpan = new TimeSpan(0, 0, minutes: 30, 0);
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