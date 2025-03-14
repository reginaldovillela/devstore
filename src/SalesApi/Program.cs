using SalesApi.Endpoints;
using SalesApi.Extensions;
using SalesApi.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddDefaultServices();
builder.AddApplicationServices();
builder.Services.RegisterInfrastructureDependencies(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.MapProductsEndpoints();
app.MapSalesEndpoints();

app.UseHttpsRedirection();
await app.RunAsync();