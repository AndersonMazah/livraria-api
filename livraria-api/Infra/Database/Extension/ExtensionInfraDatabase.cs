using Livraria.Core.Domain.Ports;
using Livraria.Infra.Database.Context;
using Livraria.Infra.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.Infra.Database.Extension;

public static class ExtensionInfraDatabase
{
    public static IServiceCollection AddExtensionInfraDatabase(this IServiceCollection services)
    {
        services.AddDbContext<LivrariaDbContext>(ServiceLifetime.Transient);
        services.AddScoped<IAutoresRepository, AutoresRepository>();
        services.AddScoped<IClientesRepository, ClientesRepository>();
        services.AddScoped<ILivrosRepository, LivrosRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
        services.AddScoped<IVendasRepository, VendasRepository>();
        services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
        return services;
    }

}
