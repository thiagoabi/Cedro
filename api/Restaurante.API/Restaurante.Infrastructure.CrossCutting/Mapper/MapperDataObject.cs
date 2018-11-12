using AutoMapper;
using Restaurante.Infrastructure.CrossCutti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante
{
    public static class MapperDataObject
    {
        public static TDestination SetMapping<TSource, TDestination>(this TSource model)
            where TDestination : IApiModel
            where TSource : class
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, IEnumerable<TDestination>>();
                cfg.RecognizePrefixes("DTO_");
            });

            IMapper mapper = new Mapper(config);
            return mapper.Map<TDestination>(model);
        }

        public static IEnumerable<TDestination> SetMapping<TSource, TDestination>(this IEnumerable<TSource> model)
            where TDestination : IApiModel
            where TSource : class
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, IEnumerable<TDestination>>();
                cfg.RecognizePrefixes("DTO_");
            });

            IMapper mapper = new Mapper(config);
            return mapper.Map<IEnumerable<TDestination>>(model);
        }
    }
}
