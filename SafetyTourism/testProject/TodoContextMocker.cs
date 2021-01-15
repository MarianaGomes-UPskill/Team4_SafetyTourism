using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Data;

namespace testProject
{
    public static class TodoContextMocker
    {
        private static WHOContext dbContext;

        public static WHOContext GetWHOContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<WHOContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            WHOContext dbContext = new WHOContext(options);
            Seed();
            return dbContext;
        }

        private static void Seed()
        {
            dbContext.TodoItems.Add();
            dbContext.TodoItems.Add();

            dbContext.SaveChanges();

        }

    }
}
