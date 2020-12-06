using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface ISaleService
    {
        Task<SaleOrderDto> CreateSaleAsync(CreateSaleInput input);
    }
}
