using System;
using System.Collections.Generic;
using System.Linq;

namespace OFClassLibrary.Common.Extensions
{
    public static class LinqRandomsExtensions
    {
        /// <summary>
        /// Return specified number of random elements from collection
        /// </summary>
        /// <param name="count">number of random elements</param>
        public static IEnumerable<T> Randoms<T>(this IEnumerable<T> source, int count)
        {
            return source.OrderBy(a => Guid.NewGuid()).Take(count);
        }

        /// <summary>
        /// Return single random element from collection
        /// </summary>
        public static T Random<T>(this IEnumerable<T> source)
        {
            return source.Randoms(1).First();
        }

        /// <summary>
        /// Return randomized collection
        /// </summary>
        public static IEnumerable<T> Randomized<T>(this IEnumerable<T> source)
        {
            return source.Randoms(source.Count());
        }
    }
}
