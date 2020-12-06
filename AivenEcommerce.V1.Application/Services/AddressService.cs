using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Mappers.Addresses;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressValidator _addressValidator;

        public AddressService(IAddressRepository addressRepository, IAddressValidator addressValidator)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _addressValidator = addressValidator ?? throw new ArgumentNullException(nameof(addressValidator));
        }

        public async Task<OperationResult<AddressDto>> CreateAddressAsync(CreateAddressInput input)
        {
            var validatioResult = await _addressValidator.ValidateCreateAddressAsync(input);

            if (validatioResult.IsSuccess)
            {
                var addresses = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);

                if (addresses is null)
                {
                    addresses = await CreateEmplyAddressCustomer(input.CustomerEmail);
                }

                var address = input.ConvertToEntity();
                address.Id = Guid.NewGuid();

                addresses.Addresses = addresses.Addresses.Add(address);

                await _addressRepository.UpdateAsync(addresses);

                return OperationResult<AddressDto>.Success(address.ConvertToDto());
            }

            return OperationResult<AddressDto>.Fail(validatioResult);
        }

        public async Task<OperationResult> DeleteAddressAsync(DeleteAddressInput input)
        {
            var validatioResult = await _addressValidator.ValidateDeleteAddressAsync(input);

            if (validatioResult.IsSuccess)
            {
                var addresses = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);

                addresses.Addresses = addresses.Addresses.Where(x => x.Id != input.AddressId);

                await _addressRepository.UpdateAsync(addresses);

                return OperationResult.Success();
            }

            return OperationResult.Fail();
        }

        public async Task<OperationResult<AddressDto>> GetAddressAsync(Guid id, string customerEmail)
        {
            var addresses = await _addressRepository.GetByCustomerAsync(customerEmail);

            if (addresses is null)
            {
                return OperationResult<AddressDto>.NotFound();
            }

            var address = addresses.Addresses.SingleOrDefault(x => x.Id == id);

            if (address is null)
            {
                return OperationResult<AddressDto>.NotFound();
            }

            return OperationResult<AddressDto>.Success(address.ConvertToDto());
        }

        public async Task<OperationResultEnumerable<AddressDto>> GetAllAsync(string customerEmail)
        {
            var addresses = await _addressRepository.GetByCustomerAsync(customerEmail);

            if (addresses is null)
            {
                return OperationResultEnumerable<AddressDto>.NotFound();
            }

            return OperationResultEnumerable<AddressDto>.Success(addresses.Addresses.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<AddressDto>> UpdateAddressAsync(UpdateAddressInput input)
        {
            var validatioResult = await _addressValidator.ValidateUpdateAddressAsync(input);

            if (validatioResult.IsSuccess)
            {
                var addresses = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);
                var address = input.ConvertToEntity();


                foreach (var e in addresses.Addresses.Where(a => a.Id == input.AddressId))
                {
                    e.Name = address.Name;
                    e.ZipCode = address.ZipCode;
                    e.City = address.City;
                    e.Street = address.Street;
                    e.Number = address.Number;
                    e.Depatarment = address.Depatarment;
                    e.BetweenStreet1 = address.BetweenStreet1;
                    e.BetweenStreet2 = address.BetweenStreet2;
                    e.Observations = address.Observations;
                    e.Phone = address.Phone;
                    e.Type = address.Type;
                    e.CustomerEmail = address.CustomerEmail;
                }

                await _addressRepository.UpdateAsync(addresses);

                return OperationResult<AddressDto>.Success(address.ConvertToDto());
            }

            return OperationResult<AddressDto>.Fail(validatioResult);
        }

        private Task<AddressCustomer> CreateEmplyAddressCustomer(string customerEmail) =>
            _addressRepository.CreateAsync(new()
            {
                CustomerEmail = customerEmail,
                Addresses = Enumerable.Empty<Address>()
            });
    }
}
