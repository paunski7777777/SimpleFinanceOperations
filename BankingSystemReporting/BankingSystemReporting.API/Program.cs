using BankingSystemReporting.API.Infrastructure.Extensions;
using BankingSystemReporting.Services.Mapping;
using BankingSystemReporting.Services.ViewModels;

using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services
     .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
     .AddDatabase(configuration)
     .AddConfigurations()
     .AddApplicationServices()
     .AddControllers()
     .AddXmlSerializerFormatters();

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

WebApplication app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

app.Run();
