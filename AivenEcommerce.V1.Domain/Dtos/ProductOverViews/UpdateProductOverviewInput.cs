using System;

namespace AivenEcommerce.V1.Domain.Dtos.ProductOverViews
{
    public record UpdateProductOverviewInput(Guid Id, string ProductId, string Description);
}
