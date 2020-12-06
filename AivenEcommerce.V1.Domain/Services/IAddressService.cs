using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IAddressService : IScopedService
    {
        Task<OperationResultEnumerable<AddressDto>> GetAllAsync(string customerEmail);
        Task<OperationResult<AddressDto>> GetAddressAsync(Guid id, string customerEmail);
        Task<OperationResult<AddressDto>> CreateAddressAsync(CreateAddressInput input);
        Task<OperationResult<AddressDto>> UpdateAddressAsync(UpdateAddressInput input);
        Task<OperationResult> DeleteAddressAsync(DeleteAddressInput input);
    }
}
