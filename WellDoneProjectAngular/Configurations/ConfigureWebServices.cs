using WellDoneProjectAngular.Core.Interfaces.Services;
using WellDoneProjectAngular.Core.Services;

namespace WellDoneProjectAngular.Configurations
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(typeof(BasketViewModelService).Assembly);

            services.AddScoped<ICatalogTypeService, CatalogTypeService>();

            //services.AddScoped<CatalogViewModelService>();
            //services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();

            return services;
        }
    }
}
