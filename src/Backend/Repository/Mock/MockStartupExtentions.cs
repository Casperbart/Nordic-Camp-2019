using Backend.Repository.Mock.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Repository.Mock
{
    public static class MockStartupExtentions
    {
        public static IServiceCollection AddMockRepository(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPageRepository, MockPageRepository>();
        }
    }
}
