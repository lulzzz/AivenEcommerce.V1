namespace AivenEcommerce.V1.Domain.Caching
{
    public record ScopedCacheKey(string Entity, string Method, string Parameter = "");
}
