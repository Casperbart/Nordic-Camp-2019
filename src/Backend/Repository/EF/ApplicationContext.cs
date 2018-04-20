using Backend.Model;
using Backend.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityRegistration> ActivityRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add key for ActivityRegistration
            modelBuilder.Entity<ActivityRegistration>().HasKey(e => new {e.UserId, e.ActivityId});
        }
    }
}
