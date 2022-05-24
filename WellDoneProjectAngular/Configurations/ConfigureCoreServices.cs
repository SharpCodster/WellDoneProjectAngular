using WellDoneProjectAngular.Core;
using WellDoneProjectAngular.Core.Interfaces;
using WellDoneProjectAngular.Core.Interfaces.Data;
using WellDoneProjectAngular.Core.Interfaces.Services;
using WellDoneProjectAngular.Core.Services;
using WellDoneProjectAngular.Infrastructure.Repositories;

namespace WellDoneProjectAngular.Configurations
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddTransient<IRequestContextProvider, RequestContextProvider>();

            

            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IBasketQueryService, BasketQueryService>();
            //services.AddSingleton<IUriComposer>(new UriComposer(configuration.Get<CatalogSettings>()));
            //services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            //services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
