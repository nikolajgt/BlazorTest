
using BlazorTest.Domain.Models;
using BlazorTest.Migration;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorTest.Infrastructure.Services;

public class UserService
{
    private readonly DatabaseContext _dbContext;
    private readonly CustomAuthenticationService _authService;
    
    public UserService(
        DatabaseContext context,
        CustomAuthenticationService authService)
    {
        _dbContext = context;
        _authService = authService;
    }


    public async Task<User?> LoginAsync(string username, string password)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.UserName == username && x.Password == password);

        if (user != null)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Claims.FirstOrDefault()) 
            }, "Custom Authentication");

            var newUser = new ClaimsPrincipal(identity);

            _authService.CurrentUser = newUser;
        }

        return user;
    }
}
