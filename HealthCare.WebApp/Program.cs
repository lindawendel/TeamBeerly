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
using Microsoft.OpenApi.Models;
using HealthCare.WebApp.Pages.Service;
using HealthCare.WebApp;
using HealthCare.WebApp.ForTestingOnly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<HealthCareContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Scoped services

builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<UserDataService>();
builder.Services.AddScoped<CaregiverService>();
builder.Services.AddScoped<ApplicationUserService>();
builder.Services.AddScoped<IMailService, InMemoryMailService>();


builder.Services.AddScoped<HttpClient>(s =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiSettings:ApiBaseUrl"]) };
    return httpClient;
});


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
    options.ResponseType = "code id_token";
    options.SaveTokens = true;
    options.Authority = $"https://{auth0Settings["Domain"]}";
    options.ClientId = auth0Settings["ClientId"];
    options.ClientSecret = auth0Settings["ClientSecret"];
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.CallbackPath = new PathString("/callback");
    options.ClaimsIssuer = "Auth0";
});

builder.Services.AddScoped<IAuthenticationService>(provider =>
    new AuthenticationService(
        provider.GetRequiredService<NavigationManager>(),
        provider.GetRequiredService<IHttpContextAccessor>(),
        provider.GetRequiredService<IConfiguration>(),
        provider.GetRequiredService<UserDataService>()));


// Building the web application

var app = builder.Build();

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

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
