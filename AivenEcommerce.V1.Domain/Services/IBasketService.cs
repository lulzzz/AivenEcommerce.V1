using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IBasketService : IScopedService
    {
        Task<OperationResult<BasketDto>> GetBasketAsync(string customerEmail);
        Task<OperationResult<BasketProductsDto>> GetBasketProductsAsync(string customerEmail);
        Task<OperationResult<BasketDto>> AddBasketProductAsync(AddBasketProductInput input);
        Task<OperationResult<BasketDto>> RemoveBasketProductAsync(RemoveBasketProductInput input);
        Task<OperationResult> RemoveAllBasketAsync(RemoveAllBasketInput input);
        Task<OperationResult<BasketDto>> UpdateBasketAsync(UpdateBasketInput input);
    }
}
