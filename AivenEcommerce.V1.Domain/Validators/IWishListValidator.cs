using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.WishLists;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IWishListValidator : IScopedService
    {
        Task<ValidationResult> ValidateAddProductToWishList(AddProductToWishListInput input);
        Task<ValidationResult> ValidateRemoveProductToWishList(RemoveProductToWishListInput input);
        Task<ValidationResult> ValidateRemoveAllWishList(RemoveAllWishListInput input);
        Task<ValidationResult> ValidateUpdateWishList(UpdateWishListInput input);
    }
}
