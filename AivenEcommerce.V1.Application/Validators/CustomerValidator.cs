using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.Customers;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Validators
{
    public class CustomerValidator : ICustomerValidator
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ValidationResult> ValidateCreateCustomer(CreateCustomerInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Name), "Debe ingresar un nombre."));
            }
            if (string.IsNullOrWhiteSpace(input.LastName))
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Name), "Debe ingresar un apellido."));
            }
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Email), "Debe ingresar un email."));
            }
            else if (!input.Email.IsEmail())
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Email), "Debe ingresar un email valido."));
            }

            if (input.Picture is null)
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Picture), "Debe ingresar una foto de perfil."));
            }

            if (!string.IsNullOrWhiteSpace(input.Email) && input.Email.IsEmail())
            {
                Customer customer = await _customerRepository.GetCustomer(input.Email);
                if (customer is not null)
                {
                    validationResult.Messages.Add(new(nameof(CreateCustomerInput.Email), "ya existe un cliente con el mismo email."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteCustomer(DeleteCustomerInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                validationResult.Messages.Add(new(nameof(DeleteCustomerInput.Email), "Debe ingresar un email."));
            }
            else if (!input.Email.IsEmail())
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Email), "Debe ingresar un email valido."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.Email);
                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(DeleteCustomerInput.Email), "no existe un cliente con este email."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateCustomer(UpdateCustomerInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                validationResult.Messages.Add(new(nameof(UpdateCustomerInput.Email), "Debe ingresar un email."));
            }
            else if (!input.Email.IsEmail())
            {
                validationResult.Messages.Add(new(nameof(CreateCustomerInput.Email), "Debe ingresar un email valido."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.Email);
                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateCustomerInput.Email), "no existe un cliente con este email."));
                }
            }

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(UpdateCustomerInput.Name), "Debe ingresar un nombre."));
            }
            if (string.IsNullOrWhiteSpace(input.LastName))
            {
                validationResult.Messages.Add(new(nameof(UpdateCustomerInput.Name), "Debe ingresar un apellido."));
            }

            if (input.Picture is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateCustomerInput.Picture), "Debe ingresar una foto de perfil."));
            }

            return validationResult;
        }
    }
}
