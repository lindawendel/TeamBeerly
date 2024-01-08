using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HealthCare.Core;
using Microsoft.EntityFrameworkCore;
using HealthCare.Core.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

// Configure SQL LocalDB
builder.Services.AddDbContext<HealthCareContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

InMemoryDbInitializer.Initialize(builder.Services.BuildServiceProvider());

// Scoped services
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<PatientService>();

// Authentication configuration
var auth0Settings = builder.Configuration.GetSection("Auth0");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = $"https://{auth0Settings["Domain"]}";
    options.ClientId = auth0Settings["ClientId"];
    options.ClientSecret = auth0Settings["ClientSecret"];
    options.ResponseType = "code";
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.CallbackPath = new PathString("/callback");
    options.ClaimsIssuer = "Auth0";
});

// Custom authentication service
builder.Services.AddScoped<IAuthenticationService>(provider =>
    new AuthenticationService(
        provider.GetRequiredService<NavigationManager>(),
        provider.GetRequiredService<IHttpContextAccessor>(),
        provider.GetRequiredService<IConfiguration>()));

// Building the web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
