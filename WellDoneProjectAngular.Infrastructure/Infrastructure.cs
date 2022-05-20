using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WellDoneProjectAngular.Infrastructure.Data;

namespace WellDoneProjectAngular.Infrastructure
{
    public static class Infrastructure
    {
        const string LocalConnectionStringName = "DefaultConnection";
        const string MigrationAssembly = "WellDoneProjectAngular.Infrastructure";

        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString(LocalConnectionStringName);

            if (!String.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(MigrationAssembly)),
                    ServiceLifetime.Scoped);
            }
        }
    }
}
