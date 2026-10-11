using Users.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteCloud.Data;

public class ByteCloudDbContext : DbContext
{
    // Represents the Users table in the database.
    public DbSet<User> Users { get; set; }
    
    // Receives the database configuration from ASP.NET Core's
    // dependency injection system and passes it to DbContext.
    public ByteCloudDbContext(
        DbContextOptions<ByteCloudDbContext>options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Prevents multiple users from registering with the same email.
        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
        
        // Prevents multiple users from registering with the same username.
        modelBuilder.Entity<User>()
        .HasIndex(u => u.Username)
        .IsUnique();
        
        // Runs EF Core's default model configuration as well.
        base.OnModelCreating(modelBuilder);
    }
}
