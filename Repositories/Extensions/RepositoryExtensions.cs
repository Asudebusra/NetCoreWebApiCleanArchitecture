using App.Repositories.Categories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    //Extensions class olarak static olmak zorunda 
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                //tip güvenli bir şekilde 
                //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")); //eski hallerde böyle olmalıydı tip güvenli olmasaydı

                options.UseSqlServer(connectionString!.SqlServer,sqlServerOptionsAction =>
                {
                    //sqlServerOptionsAction.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    //kendine özgü bir assembly classı olacak
                    sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);

                }); // datanın null olmayacağı anlamına geliyor
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
