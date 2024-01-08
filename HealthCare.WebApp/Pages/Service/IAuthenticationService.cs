using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public interface IAuthenticationService
{
    Task Login();
    Task Logout();
}

public class AuthenticationService : IAuthenticationService
{
    private readonly NavigationManager _navigationManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        NavigationManager navigationManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _navigationManager = navigationManager;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public async Task Login()
    {
        var redirectUri = _navigationManager.BaseUri + "authentication/login-callback";
        var properties = new AuthenticationProperties { RedirectUri = redirectUri };
        await _httpContextAccessor.HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
    }


    public async Task Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var postLogoutRedirectUri = _navigationManager.BaseUri;

        var logoutUrl = $"https://{domain}/v2/logout?client_id={clientId}&returnTo={Uri.EscapeDataString(postLogoutRedirectUri)}";
        _navigationManager.NavigateTo(logoutUrl, forceLoad: true);
    }
}


