using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Infra.Common;

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
                Email = "joanna",
                PasswordHash = "joanna",
                Role = ERole.Admin
            };
            db.Users.Add(joanna);

            var natasha = new User
            {
                Email = "natasha",
                PasswordHash = "natasha",
                Role = ERole.User
            };
            db.Users.Add(natasha);

            db.SaveChanges();
        }
    }
}
