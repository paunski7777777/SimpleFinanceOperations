namespace BankingSystemReporting.Services.Mapping
{
    using AutoMapper.QueryableExtensions;

    using System;
    using System.Linq.Expressions;

    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, object parameters)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters);
        }
    }
}
