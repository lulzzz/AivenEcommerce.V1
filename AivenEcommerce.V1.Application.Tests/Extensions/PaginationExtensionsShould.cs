using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Paginations;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Extensions
{
    public class PaginationExtensionsShould
    {
        [Theory]
        [InlineData(5, 5, 20)]
        [InlineData(1, null, null)]
        [InlineData(1, 50, 0)]
        [InlineData(0, 0, 0)]
        public void ConvertToEnumerable_ObjectNotNull_ReturnEnumerableWithOneItem(int pageNumber, int? pageSize, int? result)
        {
            QueryStringParameters query = new ProductParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            Assert.Equal(result, query.CalculateSkip());
        }
    }
}
