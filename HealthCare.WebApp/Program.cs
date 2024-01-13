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
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<CaregiverService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<UserDataService>();


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


// Temporary - not secure!
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthcareAPI", Version = "v1" });
});

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

app.UseCors("AllowAll"); //NOT SECURE

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthcareAPI V1");
    c.RoutePrefix = "swagger";
});



app.UseAuthentication();
app.UseAuthorization();

try
{
    InMemoryDbInitializer.Initialize(app.Services);
}
catch (Exception ex)
{
    // Log or print the exception details
    Console.WriteLine($"Exception during database initialization: {ex.Message}");
}

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
