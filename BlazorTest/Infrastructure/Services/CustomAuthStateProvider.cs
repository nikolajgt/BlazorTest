using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorTest.Infrastructure.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILogger<CustomAuthStateProvider> _logger;
    private AuthenticationState authenticationState;

    public CustomAuthStateProvider(
        ILogger<CustomAuthStateProvider> logger,
        CustomAuthenticationService service)
    {
        _logger = logger;

        authenticationState = new AuthenticationState(service.CurrentUser);
        service.UserChanged += (newUser) =>
        {
            authenticationState = new AuthenticationState(newUser);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        };
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
         Task.FromResult(authenticationState);
}
