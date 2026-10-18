using ByteCloud.Data;
using RateLimiting.Configuration;
using Users.Models;
using PasswordService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Email.Services;
using Resend;

// Creates the ASP.NET Core application builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResend(options =>
{
    options.ApiToken = builder.Configuration["Resend:ApiKey"]
        ?? throw new InvalidOperationException(
            "Resend API key is not configured.");
});

// Registers MVC controllers and Razor views with the application
builder.Services.AddControllersWithViews();

// Registers ByteCloudDbContext with ASP.NET Core's dependency injection system
// and configures EF Core to use SQLite with the bytecloud.db database file.
builder.Services.AddDbContext<ByteCloudDbContext>(
    options => options.UseSqlite("Data Source=bytecloud.db")
);

// Registers ASP.NET Core Identity to manage users and their authentication data.
builder.Services
    .AddIdentityCore<User>(options =>
    {
        // Requires each account to have a unique email address.
        options.User.RequireUniqueEmail = true;

        // Configures Identity sign-in to require a confirmed email address.
        options.SignIn.RequireConfirmedEmail = true;
    })
    // Registers role-management services using integer role IDs.
    .AddRoles<IdentityRole<int>>()
    // Connects Identity to the existing ByteCloudDbContext and SQLite database.
    .AddEntityFrameworkStores<ByteCloudDbContext>()
    // Registers SignInManager, which helps handle user sign-in.
    .AddSignInManager()
    // Registers built-in token providers for email confirmation and password resets.
    .AddDefaultTokenProviders();

// Registers PasswordServices with ASP.NET Core's dependency injection system.
// Kept temporarily while the existing authentication code is being migrated to Identity.
builder.Services.AddScoped<PasswordServices>();

builder.Services.AddScoped<EmailService>();

// Registers ByteCloud's existing rate-limiting policies.
builder.Services.AddByteCloudRateLimiting();

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

// Enables ASP.NET Core's built-in rate-limiting middleware.
app.UseRateLimiter();

// Enables authorization checks for protected resources
app.UseAuthorization();

// Enables static files such as CSS, JavaScript, and images
app.MapStaticAssets();

// Defines the default MVC controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();

// Creates a simple health-check endpoint
// Visiting / returns "Server is running"
app.MapGet("/", () =>
{
    return "Server is running";
});

// Starts the ASP.NET Core application and begins listening for requests
app.Run();