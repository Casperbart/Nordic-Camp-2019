using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Backend.Repository.EF
{
    /// <summary>
    /// Class used for setting up temp database for usage when running dotnet ef migrate commands
    /// </summary>
    public class ApplicationContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var contextBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            contextBuilder.UseSqlite("Data Source=Migrations.db");
            return new ApplicationContext(contextBuilder.Options);
        }
    }
}
