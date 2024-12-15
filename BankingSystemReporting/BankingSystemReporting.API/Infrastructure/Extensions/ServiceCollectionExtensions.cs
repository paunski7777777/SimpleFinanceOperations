namespace BankingSystemReporting.API.Infrastructure.Extensions
{
    using BankingSystemReporting.Services.Contracts;
    using BankingSystemReporting.Services;
    using BankingSystemReporting.Data;
    using BankingSystemReporting.Common.Constants;

    using Microsoft.EntityFrameworkCore;

    using CsvHelper.Configuration;

    using System.Globalization;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
            .AddDbContext<AppDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString(AppConstants.Configurations.Database.ConnectionString),
                    opt => opt.CommandTimeout(AppConstants.Configurations.Database.CommandTimeout)));

        public static IServiceCollection AddConfigurations(this IServiceCollection services)
            => services
            .AddSingleton(new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = AppConstants.CSV.HasHeaderRecord,
                Delimiter = AppConstants.CSV.Delimeter,
            });

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
            .AddScoped<IPartnersService, PartnersService>()
            .AddScoped<IMerchantsService, MerchantsService>()
            .AddScoped<ITransactionsService, TransactionsService>()
            .AddScoped<IImportService, ImportService>()
            .AddScoped<IConvertersService, ConvertersService>()
            .AddScoped<IExportsService, ExportsService>();
    }
}
