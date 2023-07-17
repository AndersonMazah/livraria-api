using AutoMapper;
using Livraria.Core.Application.Dtos;
using Livraria.Core.Application.Ports;
using Livraria.Core.Application.Service;
using Livraria.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Livraria.Core.Application.Extension;

public static class ExtensionCoreApplication
{
    public static IServiceCollection AddExtensionCoreApplication(this IServiceCollection services)
    {
        services.AddScoped<IAutoresApplication, AutoresApplication>();
        services.AddScoped<IClientesApplication, ClientesApplication>();
        services.AddScoped<ILivrosApplication, LivrosApplication>();
        services.AddScoped<IVendasApplication, VendasApplication>();
        services.AddScoped<IAvaliacaoApplication, AvaliacaoApplication>();

        MapperConfiguration config = new MapperConfiguration(config =>
        {
            config.CreateMap<Autores, AutoresDto>().ReverseMap();
            config.CreateMap<Clientes, ClientesDto>().ReverseMap();
            config.CreateMap<Livros, LivrosDto>().ReverseMap();
            config.CreateMap<Livros, InfoLivroDto>().ReverseMap();
            config.CreateMap<Avaliacao, AvaliacaoDto>().ReverseMap();
            config.CreateMap<Vendas, VendasDto>().ReverseMap();
        });
        IMapper mapper = config.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

}
