using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Validators
{
    public class AddressValidator : IAddressValidator
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;

        public AddressValidator(IAddressRepository addressRepository, ICustomerRepository customerRepository)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ValidationResult> ValidateCreateAddressAsync(CreateAddressInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(CreateAddressInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(CreateAddressInput.CustomerEmail), "No existe el cliente."));
                }
            }

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(CreateAddressInput.Name), "Debe ingresar un nombre."));
            }

            if (string.IsNullOrWhiteSpace(input.ZipCode))
            {
                validationResult.Messages.Add(new(nameof(CreateAddressInput.ZipCode), "Debe ingresar un código postal."));
            }
            if (string.IsNullOrWhiteSpace(input.City))
            {
                validationResult.Messages.Add(new(nameof(CreateAddressInput.City), "Debe ingresar una ciudad."));
            }
            if (string.IsNullOrWhiteSpace(input.Street))
            {
                validationResult.Messages.Add(new(nameof(CreateAddressInput.Street), "Debe ingresar una calle."));
            }

            if (!Enum.IsDefined(input.Type))
            {
                validationResult.Messages.Add(new(nameof(CreateAddressInput.Type), "El tipo de dirección no es valido."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteAddressAsync(DeleteAddressInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(DeleteAddressInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(DeleteAddressInput.CustomerEmail), "No existe el cliente."));
                }

                AddressCustomer addressCustomer = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);

                if (addressCustomer is null || addressCustomer.Addresses is null || !addressCustomer.Addresses.Any(x => x.Id == input.AddressId))
                {
                    validationResult.Messages.Add(new(nameof(DeleteAddressInput.AddressId), "La dirección no existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateAddressAsync(UpdateAddressInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(UpdateAddressInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateAddressInput.CustomerEmail), "No existe el cliente."));
                }

                AddressCustomer addressCustomer = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);

                if (addressCustomer is null || addressCustomer.Addresses is null || !addressCustomer.Addresses.Any(x => x.Id == input.AddressId))
                {
                    validationResult.Messages.Add(new(nameof(UpdateAddressInput.AddressId), "La dirección no existe."));
                }
            }

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(UpdateAddressInput.Name), "Debe ingresar un nombre."));
            }

            if (string.IsNullOrWhiteSpace(input.ZipCode))
            {
                validationResult.Messages.Add(new(nameof(UpdateAddressInput.ZipCode), "Debe ingresar un código postal."));
            }
            if (string.IsNullOrWhiteSpace(input.City))
            {
                validationResult.Messages.Add(new(nameof(UpdateAddressInput.City), "Debe ingresar una ciudad."));
            }
            if (string.IsNullOrWhiteSpace(input.Street))
            {
                validationResult.Messages.Add(new(nameof(UpdateAddressInput.Street), "Debe ingresar una calle."));
            }

            if (!Enum.IsDefined(input.Type))
            {
                validationResult.Messages.Add(new(nameof(UpdateAddressInput.Type), "El tipo de dirección no es valido."));
            }

            return validationResult;
        }
    }
}
