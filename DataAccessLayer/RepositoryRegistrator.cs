using DataAccessLayer.Entities;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Client>, DbRepository<Client>>()
            .AddTransient<IRepository<Product>, DbRepository<Product>>()
            .AddTransient<IRepository<Purchase>, DbRepository<Purchase>>()
            ;
    }
}
