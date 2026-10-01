using ByteCloud.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteCloud.Data;

public class ByteCloudDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public ByteCloudDbContext(
        DbContextOptions<ByteCloudDbContext>options)
        : base(options)
    {
    }
}