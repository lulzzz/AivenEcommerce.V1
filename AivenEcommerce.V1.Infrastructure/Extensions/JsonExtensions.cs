using System.Text.Json;

namespace AivenEcommerce.V1.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        public static string Serialize<T>(this T instance)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(instance, options);
        }

        public static T Deserialize<T>(this string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
