﻿using System.Collections.Generic;

namespace AivenEcommerce.V1.Application.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ConvertToEnumerable<T>(this T obj)
        {
            return new[] { obj };
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }

            yield return value;
        }
    }
}
