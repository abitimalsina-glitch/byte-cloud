using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ByteCloud.Data;

public class ByteCloudDbContextFactory : IDesignTimeDbContextFactory<ByteCloudDbContext>
{
    public ByteCloudDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ByteCloudDbContext>();
        optionsBuilder.UseSqlite("Data Source=bytecloud.db");

        return new ByteCloudDbContext(optionsBuilder.Options);
    }
}

