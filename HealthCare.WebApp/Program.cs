using Microsoft.EntityFrameworkCore;
using HealthCare.Core;
using HealthCare.Core.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using HealthCare.WebApp.Pages.Service;
using HealthCare.WebApp.Pages.SingeltonTest;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Database
builder.Services.AddDbContext<HealthCareContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication
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

// Blazor and SignalR
builder.Services.AddSignalR();
builder.Services.AddSingleton<GridService>();
builder.Services.AddSingleton<HubConnectionBuilder>();


// HttpClient
builder.Services.AddScoped<HttpClient>(s =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiSettings:ApiBaseUrl"]) };
    return httpClient;
});

// Cors (Temporary - not secure!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// Swagger
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthcareAPI", Version = "v1" });
});*/

// Scoped services
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<CaregiverService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<UserDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Razor Pages and Server-Side Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware
app.UseCors("AllowAll"); // NOT SECURE



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<GridHub>("/gridHub");
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

// Database Initialization
try
{
    InMemoryDbInitializer.Initialize(app.Services);
}
catch (Exception ex)
{
    Console.WriteLine($"Exception during database initialization: {ex.Message}");
}

/*app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthcareAPI V1");
    c.RoutePrefix = "swagger";
});*/

app.Run();
