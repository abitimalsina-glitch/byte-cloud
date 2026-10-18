
using Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteCloud.Data;

public class ByteCloudDbContext
    : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ByteCloudDbContext(
        DbContextOptions<ByteCloudDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Reuse the existing Users table.
        modelBuilder.Entity<User>().ToTable("Users");

        // Preserve the existing database column name.
        modelBuilder.Entity<User>()
            .Property(user => user.UserName)
            .HasColumnName("Username");

        // Keep usernames and emails unique.
        modelBuilder.Entity<User>()
            .HasIndex(user => user.UserName)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}
