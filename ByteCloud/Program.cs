// Imports the ByteCloud.Data namespace so we can use ByteCloudDbContext
using ByteCloud.Data;

// Imports EF Core functionality such as UseSqlite()
using Microsoft.EntityFrameworkCore;

// Creates the ASP.NET Core application builder
var builder = WebApplication.CreateBuilder(args);

// Registers MVC controllers and Razor views with the application
builder.Services.AddControllersWithViews();

// Registers ByteCloudDbContext with ASP.NET Core's dependency injection system
// and configures EF Core to use SQLite with the bytecloud.db database file.
builder.Services.AddDbContext<ByteCloudDbContext>(options => 
options.UseSqlite("Data Source=bytecloud.db"));

// Builds the configured ASP.NET Core application
var app = builder.Build();

// Configures the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Shows a friendly error page when the application is running in production
    app.UseExceptionHandler("/Home/Error");

    // Tells browsers to use HTTPS for future requests
    app.UseHsts();
}

// Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enables URL routing so requests can be matched to controllers and endpoints
app.UseRouting();

// Enables authorization checks for protected resources
app.UseAuthorization();

// Enables static files such as CSS, JavaScript, and images
app.MapStaticAssets();

// Defines the default MVC controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();

// Creates a simple health-check endpoint
// Visiting /health returns "Server is running"
app.MapGet("/", () =>
{
    return "Server is running";
});

// Starts the ASP.NET Core application and begins listening for requests
app.Run();