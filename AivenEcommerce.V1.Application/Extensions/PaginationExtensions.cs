using AivenEcommerce.V1.Domain.Shared.Paginations;

namespace AivenEcommerce.V1.Application.Extensions
{
    public static class PaginationExtensions
    {
        public static int? CalculateSkip(this QueryStringParameters parameters)
        {
            return (parameters.PageNumber - 1) * parameters.PageSize;
        }
    }
}
