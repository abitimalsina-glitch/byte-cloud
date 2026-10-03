using ByteCloud.Data;
using ByteCloud.Models;
using ByteCloud.Services;
using Microsoft.EntityFrameworkCore;

ByteCloudDbContext context = new ByteCloudDbContext(
    new DbContextOptionsBuilder<ByteCloudDbContext>()
        .UseSqlite("Data Source=bytecloud.db")
        .Options
);

PasswordServices passwordServices = new PasswordServices();

User user = new User
{
    Username = "Abi",
    Email = "abi@example.com",
    CreatedAt = DateTime.UtcNow,
    StorageLimit = 10737418240,
    StorageUsed = 0
};

user.PasswordHash = passwordServices.HashPassword(
    user,
    "password123"
);

context.Users.Add(user);
context.SaveChanges();

Console.WriteLine("User created.");