using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BusinessAccessLayer.Intrefaces;
using BusinessAccessLayer.Middleware;
using BusinessAccessLayer.Services;
using Serilog;
using BusinessAccessLayer.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDriveInfo, DriveInfoService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ICrud, CrudService>();

//Handle Global Exception using Filters 
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(MyExceptionFilter));
});

//log using Serilog work for both middleware and filters
builder.Host.UseSerilog((hostingContext, loggerconfig) =>
{
    loggerconfig.ReadFrom.Configuration(hostingContext.Configuration);
});

var app = builder.Build();

//Handle Global Exception using middleware 
//app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

//configure Serilog in middleware Pipeline
app.UseSerilogRequestLogging();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
