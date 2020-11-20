using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Mappers.Customers;
using AivenEcommerce.V1.Domain.Dtos.Customers;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerValidator _customerValidator;

        public CustomerService(ICustomerRepository customerRepository, ICustomerValidator customerValidator)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerValidator = customerValidator ?? throw new ArgumentNullException(nameof(customerValidator));
        }

        public async Task<OperationResult<CustomerDto>> CreateCustomerAsync(CreateCustomerInput input)
        {
            var validationResult = await _customerValidator.ValidateCreateCustomer(input);
            if (validationResult.IsSuccess)
            {
                var entity = input.ConvertToEntity();

                entity = await _customerRepository.CreateAsync(entity);

                return OperationResult<CustomerDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<CustomerDto>.Fail(validationResult);
        }

        public async Task<OperationResult> DeleteCustomerAsync(DeleteCustomerInput input)
        {
            var validationResult = await _customerValidator.ValidateDeleteCustomer(input);
            if (validationResult.IsSuccess)
            {
                var entity = await _customerRepository.GetCustomer(input.Email);
                await _customerRepository.RemoveAsync(entity);

                return OperationResult.Success();
            }

            return OperationResult<CustomerDto>.Fail(validationResult);
        }

        public async Task<OperationResultEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return OperationResultEnumerable<CustomerDto>.Success(customers.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<CustomerDto>> GetCustomerAsync(string email)
        {
            var customer = await _customerRepository.GetCustomer(email);

            return OperationResult<CustomerDto>.Success(customer.ConvertToDto());
        }

        public async Task<OperationResult<CustomerDto>> UpdateCustomerAsync(UpdateCustomerInput input)
        {
            var validationResult = await _customerValidator.ValidateUpdateCustomer(input);
            if (validationResult.IsSuccess)
            {
                var entity = await _customerRepository.GetCustomer(input.Email);
                entity.LastName = input.LastName;
                entity.Name = input.Name;
                entity.Picture = input.Picture;

                await _customerRepository.UpdateAsync(entity);

                return OperationResult<CustomerDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<CustomerDto>.Fail(validationResult);
        }
    }
}
