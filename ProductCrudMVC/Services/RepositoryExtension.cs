using ProductCrudMVC.Services.Interfaces;
using ProductCrudMVC.Services.Repositories;

namespace ProductCrudMVC.Services
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
