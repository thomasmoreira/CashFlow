using BCrypt.Net;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using static BCrypt.Net.BCrypt;

namespace CashFlow.Infra.Persistence
{
    public static class DataInitializer
    {
        public async static void Run(CashFlowDbContext db)
        {
            await db.Database.EnsureCreatedAsync();

            if (db.Users.Any()) return;

            // create user
            var joanna = new User
            {
                Username = "admin",
                PasswordHash = HashPassword("Pass@word123!"),
                Role = ERole.Manager
            };
            db.Users.Add(joanna);

            var natasha = new User
            {
                Username = "user",
                PasswordHash = HashPassword("123456"),
                Role = ERole.Employee
            };
            db.Users.Add(natasha);

            db.SaveChanges();
        }
    }
}
