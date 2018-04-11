using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Repository.EF
{
    public class DatabaseConfig
    {
        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            // Get context
            var context = serviceProvider.GetService<ApplicationContext>();
            if (context == null)
                return;

            // Migrate database
            context.Database.Migrate();

            // Add some data
            if (!context.Pages.Any())
            {
                context.Pages.AddRange(new Page
                {
                    Url = "About",
                    Content = "# About Nordic 4H Camp\nDatabase loaded"
                });
            }

            // Save changes
            context.SaveChanges();
        }
    }
}
