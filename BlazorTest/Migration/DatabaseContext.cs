using BlazorTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace BlazorTest.Migration;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}