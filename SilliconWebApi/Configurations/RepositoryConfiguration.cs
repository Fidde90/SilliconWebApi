using Infrastructure.Repositories;

namespace SilliconWebApi.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SubscriberRepository>();
            services.AddScoped<CourseRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ContactRepository>();
        }
    }
}
