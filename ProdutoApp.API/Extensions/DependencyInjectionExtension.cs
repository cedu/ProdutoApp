using ProdutoApp.Domain.Interfaces.Repositories;
using ProdutoApp.Domain.Interfaces.Services;
using ProdutoApp.Domain.Services;
using ProdutoApp.Infra.Data.Repositories;

namespace ProdutoApp.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services) 
        {
            services.AddScoped<IProdutoDomainService, ProdutoDomainService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
