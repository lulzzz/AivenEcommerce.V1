﻿namespace AivenEcommerce.V1.Application.Extensions
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
