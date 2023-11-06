// MagicVilla_Web namespace contains all the classes related to the MagicVilla web application.
using MagicVilla_Web;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
// Authentication cookies are used to manage user sessions after they log in.
using Microsoft.AspNetCore.Authentication.Cookies;

// Entry point for the web application, setting up the configuration and services.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// AutoMapper is used to map objects of one type to another, simplifying object transformations.
builder.Services.AddAutoMapper(typeof(MappingConfig));


// Registering HTTP client services for making HTTP requests to external APIs.
builder.Services.AddHttpClient<IVillaService, VillaService>();
builder.Services.AddScoped<IVillaService, VillaService>();

builder.Services.AddHttpClient<IVillaNumberService, VillaNumberService>();
builder.Services.AddScoped<IVillaNumberService, VillaNumberService>();

// Singleton service for accessing the HTTP context, useful for getting request/response information.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registering the authentication service with HTTP client and scoped lifetime.
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Distributed cache setup, which can be used for storing session data, caching responses, etc.
builder.Services.AddDistributedMemoryCache();

// Setting up cookie-based authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Ensures that the cookie is accessible only through the HTTP protocol.
        options.Cookie.HttpOnly = true;
        // Sets the expiration time for the authentication cookie.
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        // Specifies the path for the login page.
        options.LoginPath = "/Auth/Login";
        // Specifies the path to redirect to when access is denied.
        options.AccessDeniedPath = "/Auth/AccessDenied";
        // Allows the cookie to be refreshed if there is activity.
        options.SlidingExpiration = true;
    });

// Setting up session management.
builder.Services.AddSession(options =>
{
    // Sets the time after which the session will expire if there's no activity.
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    // Ensures that the session cookie is accessible only through the HTTP protocol.
    options.Cookie.HttpOnly = true;
    // Marks the session cookie as essential for the application to function.
    options.Cookie.IsEssential = true;
});

// Building the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use a custom error handler in production.
    app.UseExceptionHandler("/Home/Error");
    // Enforces strict transport security by only allowing HTTPS traffic.
    app.UseHsts();
}

// Enforces the use of HTTPS to secure the app by redirecting HTTP requests to HTTPS.
app.UseHttpsRedirection();
// Enables serving static files like images, CSS, and JavaScript.
app.UseStaticFiles();

// Adds routing capabilities to the app.
app.UseRouting();
// Enables authentication capabilities.
app.UseAuthentication();
// Enables authorization capabilities.
app.UseAuthorization();
// Enables session management.
app.UseSession();
// Defines the default route for the application.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Starts the application.
app.Run();
