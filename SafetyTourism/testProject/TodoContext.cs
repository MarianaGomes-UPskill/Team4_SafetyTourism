using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testProject
{
    class TodoContext : DbContext
    {
        private static TodoContext dbContext;

        public static TodoContext GetTodoContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }
    }
}
    