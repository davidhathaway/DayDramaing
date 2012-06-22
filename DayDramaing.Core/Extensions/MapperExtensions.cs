using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Innovations.Core.Extensions
{
    public static class MapperExtensions
    {

        public static TDest Map<TSource, TDest>(this TSource source, TDest dest)
            where TDest : class
        {
            return Mapper.Map<TSource, TDest>(source, dest);
        }

        public static TDest Map<TDest>(this object source, TDest dest)
            where TDest : class
        {

            var sourceT = source.GetType();
            var destT = typeof(TDest);
            return (TDest)Mapper.Map(source, dest, sourceT, destT);
        }

        public static TDest Map<TDest>(this object source)
            where TDest : class
        {



            return Mapper.Map<TDest>(source);
        }
    }
}
