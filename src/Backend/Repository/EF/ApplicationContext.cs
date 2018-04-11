using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Page> Pages { get; set; }
    }
}
