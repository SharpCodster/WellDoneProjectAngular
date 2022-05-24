using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Infrastructure.Data
{
    // add-migration {name} -Context ApplicationDbContext -o Data\Migrations
    //update-database -Context ApplicationDbContext
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        //public DbSet<Basket> Baskets { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<BasketItem> BasketItems { get; set; }


        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //"AspNetRoleClaims
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("IdentityRoleClaims", schema: "auth");
            });
            //"AspNetRoles"
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "IdentityRoles", schema: "auth");
            });
            //"AspNetUserClaims"
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("IdentityUserClaims", schema: "auth");
            });
            //"AspNetUserLogins
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("IdentityUserLogins", schema: "auth");
            });
            //"AspNetUserRoles"
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("IdentityUserUserRoles", schema: "auth");
            });
            //"AspNetUsers"
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "ApplicationUsers", schema: "auth");
            });
            //"AspNetUserTokens"
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("IdentityUserTokens", schema: "auth");
            });
            //"DeviceCodes"
            builder.Entity<DeviceFlowCodes>(entity =>
            {
                entity.ToTable("DeviceFlowCodes", schema: "auth");
            });
            //"PersistedGrants"
            builder.Entity<PersistedGrant>(entity =>
            {
                entity.ToTable("PersistedGrants", schema: "auth");
            });
            //"Keys"
            builder.Entity<Key>(entity =>
            {
                entity.ToTable("Keys", schema: "auth");
            });

            Assembly ass = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(ass);
        }
    }
}
