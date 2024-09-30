using BlazorTest.Components;
using BlazorTest.Domain.Models;
using BlazorTest.Infrastructure.Services;
using BlazorTest.Migration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));


// user part
builder.Services.AddScoped<UserService>();

// Authentication
builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    SeedData(dbContext);
}


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();






// temp seed
void SeedData(DatabaseContext context)
{
    var users = new List<User>
    {
        new User
        {
            Id = Guid.NewGuid(),
            UserName = "TestUser",
            Password = "TestUser", 
            Claims = new List<string>
            {
                "User",
            }
        },
        new User
        {
            UserName = "TestAdmin",
            Password = "TestAdmin",
            Claims = new List<string>
            {
                "Admin",
            }
        },
    };

    context.Users.AddRange(users);
    context.SaveChanges();
}