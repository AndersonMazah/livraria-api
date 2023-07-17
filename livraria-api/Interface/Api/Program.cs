using System.Text.Json.Serialization;
using Livraria.Core.Application.Extension;
using Livraria.Infra.Database.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExtensionInfraDatabase();
builder.Services.AddExtensionCoreApplication();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
