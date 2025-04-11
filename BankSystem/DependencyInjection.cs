using Services.Services;

namespace BankSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
