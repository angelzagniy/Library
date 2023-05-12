using Library.Web.BusinessLogic.Repository;
using Library.Web.BusinessLogic.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

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
			.AddTransient<IBooksRepository, BooksRepository>()
			.AddControllersWithViews();

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