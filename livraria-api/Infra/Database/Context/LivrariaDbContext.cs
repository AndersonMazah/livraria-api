using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Livraria.Infra.Database.Context;

public class LivrariaDbContext : AbstractContext
{
    protected readonly IConfiguration _configuration;

    // public DbSet<Autores> Autores { get; set; }

    // public DbSet<Clientes> Clientes { get; set; }

    // public DbSet<Livros> Livros { get; set; }

    // public DbSet<Vendas> Vendas { get; set; }


    public LivrariaDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("ConexaoPadrao"));
        //options.LogTo(Console.WriteLine);
        //DbContextOptionsBuilder.EnableSensitiveDataLogging.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //base.OnModelCreating(modelBuilder);
    }

}
