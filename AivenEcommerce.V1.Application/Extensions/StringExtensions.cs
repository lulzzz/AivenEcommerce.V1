using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool HasFileInvalidChars(this string source)
        {
            return source.Contains('<') ||
                source.Contains('>') ||
                source.Contains(':') ||
                source.Contains('"') ||
                source.Contains('/') ||
                source.Contains('\\') ||
                source.Contains('|') ||
                source.Contains('*') ||
                source.Contains('?');
        }
    }
}
