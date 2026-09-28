// Creates the ASP.NET Core application builder
var builder = WebApplication.CreateBuilder(args);

// Registers MVC controllers and views
builder.Services.AddControllersWithViews();

// Builds the application
var app = builder.Build();

// Configure the request pipeline
if (!app.Environment.IsDevelopment())
{
    // Shows a friendly error page in production
    app.UseExceptionHandler("/Home/Error");

    // Forces browsers to use HTTPS
    app.UseHsts();
}

// Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enables URL routing
app.UseRouting();

// Enables authorization checks
app.UseAuthorization();

// Enables static files like CSS and JavaScript
app.MapStaticAssets();

// Defines the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();

app.MapGet("/health", () =>
{
    return "Server is running";
});

// Starts the application
app.Run();