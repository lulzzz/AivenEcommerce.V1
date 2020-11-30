
using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.ProductOverViews
{
    public record UpdateProductOverviewInput(Guid Id, string ProductId, string Description);
}
