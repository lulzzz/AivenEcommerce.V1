using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Baskets;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IBasketService
    {
        Task<OperationResult<BasketDto>> GetBasketAsync(string customerEmail);
        Task<OperationResult<BasketDto>> AddBasketProductAsync(AddBasketProductInput input);
        Task<OperationResult<BasketDto>> RemoveBasketProductAsync(RemoveBasketProductInput input);
        Task<OperationResult> RemoveAllBasketAsync(RemoveAllBasketInput input);
        Task<OperationResult<BasketDto>> UpdateBasketAsync(UpdateBasketInput input);
    }
}
