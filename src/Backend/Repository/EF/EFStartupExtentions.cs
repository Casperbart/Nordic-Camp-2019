using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Repository.EF.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Repository.EF
{
    public static class EfStartupExtentions
    {
        public static IServiceCollection AddEfRepository(this IServiceCollection services)
        {
            return services.AddScoped<IPageRepository, EfPageRepository>()
                .AddScoped<IGenericRepository<Page>>(s => s.GetService<IPageRepository>());
        }
    }
}
