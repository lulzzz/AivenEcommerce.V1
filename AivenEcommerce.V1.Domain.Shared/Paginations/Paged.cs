namespace AivenEcommerce.V1.Domain.Shared.Paginations
{
    public record Paged(int CurrentPage, long TotalCount, long TotalPages, int PageSize);
}
