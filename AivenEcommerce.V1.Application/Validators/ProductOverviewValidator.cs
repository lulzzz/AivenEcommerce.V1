using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Validators
{
    public class ProductOverviewValidator : IProductOverviewValidator
    {
        private readonly IProductRepository _productRepository;

        public ProductOverviewValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ValidationResult> ValidateCreateProductOverview(CreateProductOverviewInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(CreateProductOverviewInput.ProductId), "El producto no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductOverview(UpdateProductOverviewInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductOverviewInput.ProductId), "El producto no existe."));
            }

            return validationResult;
        }
    }
}
