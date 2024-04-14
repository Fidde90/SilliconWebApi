using Infrastructure.Services;

namespace SilliconWebApi.Configurations
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CategoryService>();
            services.AddScoped<CourseService>();
            services.AddScoped<SubscriberService>();
            services.AddScoped<ContactMessageService>();
        }
    }
}
