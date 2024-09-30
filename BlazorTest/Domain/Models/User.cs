using System.Security.Claims;

namespace BlazorTest.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public IList<string>? Claims { get; set; }
}
