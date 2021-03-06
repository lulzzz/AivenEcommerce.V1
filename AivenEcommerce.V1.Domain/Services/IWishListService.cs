﻿using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.WishLists;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IWishListService : IScopedService
    {
        Task<OperationResult<WishListDto>> GetWishListAsync(string customerEmail);
        Task<OperationResult<WishListProductsDto>> GetWishListWithProductInfoAsync(string customerEmail);
        Task<OperationResult<WishListDto>> AddProductToWishListAsync(AddProductToWishListInput input);
        Task<OperationResult<WishListDto>> RemoveProductToWishListAsync(RemoveProductToWishListInput input);
        Task<OperationResult<WishListDto>> RemoveAllWishListAsync(RemoveAllWishListInput input);
        Task<OperationResult<WishListDto>> UpdateWishListAsync(UpdateWishListInput input);
    }
}
