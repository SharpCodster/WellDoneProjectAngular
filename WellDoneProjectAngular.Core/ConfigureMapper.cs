using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WellDoneProjectAngular.Core
{
    public static class ConfigureMapper
    {
        private static MapperConfiguration configs;

        public static IConfigurationProvider GetConfiguration()
        {
            if (configs == null)
            {
                configs = new MapperConfiguration(cfg => { cfg.AddMaps(Assembly.GetExecutingAssembly()); });
            }
            return configs;
        }

        public static void ConfigAutoMapper()
        {
            var configuration = GetConfiguration();
            configuration.AssertConfigurationIsValid();
        }

        public static IServiceCollection AddAutoMapper(IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(ConfigureMapper));
        }

    }
}
