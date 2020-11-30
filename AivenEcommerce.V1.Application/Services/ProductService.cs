using AivenEcommerce.V1.Application.Mappers.Paginations;
using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Shared.Paginations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductOverviewRepository _overviewRepository;
        private readonly IProductBadgeRepository _badgeRepository;
        private readonly IProductImageRepository _imageRepository;
        private readonly IProductValidator _validator;

        public ProductService(IProductRepository repository, IProductOverviewRepository overviewRepository, IProductBadgeRepository badgeRepository, IProductImageRepository imageRepository, IProductValidator validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _overviewRepository = overviewRepository ?? throw new ArgumentNullException(nameof(overviewRepository));
            _badgeRepository = badgeRepository ?? throw new ArgumentNullException(nameof(badgeRepository));
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<OperationResult<ProductDto>> CreateAsync(CreateProductInput input)
        {
            var validationResult = await _validator.ValidateCreateProduct(input);

            if (validationResult.IsSuccess)
            {
                Product product = input.ConvertToEntity();

                product = await _repository.CreateAsync(product);

                Task overviewTask = _overviewRepository.CreateAsync(new()
                {
                    ProductId = product.Id,
                    Description = input.Description
                });

                Task badgeTask = _badgeRepository.CreateAsync(new()
                {
                    ProductId = product.Id,
                    Badges = Enumerable.Empty<ProductBadgeName>()
                });

                Task.WaitAll(overviewTask, badgeTask);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }

        }

        public async Task<OperationResult> DeleteAsync(DeleteProductInput input)
        {
            var validationResult = await _validator.ValidateDeleteProduct(input);

            if (validationResult.IsSuccess)
            {
                await _repository.RemoveAsync(input.Id);

                Task overviewTask = Task.Factory.StartNew(async () =>
                {
                    var overview = await _overviewRepository.GetByProduct(new() { Id = input.Id });
                    if (overview is not null)
                        await _overviewRepository.RemoveAsync(overview);
                });

                Task badgeTask = Task.Factory.StartNew(async () =>
                {
                    var badge = await _badgeRepository.GetByProduct(new() { Id = input.Id });
                    if (badge is not null)
                        await _badgeRepository.RemoveAsync(badge);
                });

                Task imageTask = Task.Factory.StartNew(async () =>
                {
                    var images = await _imageRepository.GetProductImages(new() { Id = input.Id });
                    if (images is not null)
                        await _imageRepository.RemoveAsync(new ProductImage { ProductId = input.Id });
                });

                Task.WaitAll(overviewTask, badgeTask, imageTask);

                return OperationResult.Success();
            }
            else
            {
                return OperationResult.Fail(validationResult);
            }

        }

        public async Task<OperationResult<ProductDto>> GetAsync(string id)
        {
            Product product = await _repository.GetAsync(id);

            if (product is null)
            {
                return OperationResult<ProductDto>.NotFound();
            }

            return OperationResult<ProductDto>.Success(product.ConvertToDto());
        }

        public async Task<OperationResultEnumerable<ProductDto>> GetAllAsync()
        {
            IEnumerable<Product> products = await _repository.GetAllAsync();

            return OperationResultEnumerable<ProductDto>.Success(products.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<PagedResult<ProductDto>>> GetAllAsync(ProductParameters parameters)
        {
            PagedData<Product> pagedData = await _repository.GetAllAsync(parameters);

            PagedData<ProductDto> pagedDataDto = pagedData.ConvertToDto(x => x.ConvertToDto());

            return OperationResult<PagedResult<ProductDto>>.Success(new(pagedDataDto, parameters));
        }

        public async Task<OperationResult<ProductDto>> UpdateAsync(UpdateProductInput input)
        {
            var validationResult = await _validator.ValidateUpdateProduct(input);

            if (validationResult.IsSuccess)
            {
                Product product = input.ConvertToEntity();

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateMainImageAsync(UpdateProductMainImageInput input)
        {
            Product product = await _repository.GetAsync(input.ProductId);

            if (product is null)
            {
                return OperationResult<ProductDto>.NotFound();
            }

            product.Thumbnail = input.Image;

            await _repository.UpdateAsync(product);

            return OperationResult<ProductDto>.Success(product.ConvertToDto());
        }

        public async Task<OperationResult<ProductDto>> UpdateProductCostPriceAsync(UpdateProductCostPriceInput input)
        {
            var validationResult = await _validator.ValidateUpdateProductCostPrice(input);

            if (validationResult.IsSuccess)
            {
                Product product = await _repository.GetAsync(input.ProductId);

                product.Cost = input.Cost;
                product.Price = input.Price;

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateProductCategoryAsync(UpdateProductCategorySubCategoryInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProductCategory(input);

            if (validationResult.IsSuccess)
            {
                Product product = await _repository.GetAsync(input.ProductId);

                product.Category = input.Category;
                product.SubCategory = input.SubCategory;

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateProductAvailabilityAsync(UpdateProductAvailabilityInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProductAvailability(input);

            if (validationResult.IsSuccess)
            {
                Product product = await _repository.GetAsync(input.ProductId);

                product.IsActive = input.IsActive;
                product.Stock = input.Stock;

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateProductNameDescriptionAsync(UpdateProductNameDescriptionInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProductNameDescription(input);

            if (validationResult.IsSuccess)
            {
                Product product = await _repository.GetAsync(input.ProductId);

                product.Name = input.Name;

                await _repository.UpdateAsync(product);

                ProductOverview productOverview = await _overviewRepository.GetByProduct(product);

                if (productOverview is null)
                {
                    productOverview = new()
                    {
                        ProductId = product.Id,
                        Description = input.Description
                    };
                    await _overviewRepository.CreateAsync(productOverview);
                }
                else
                {
                    productOverview.Description = input.Description;
                    await _overviewRepository.UpdateAsync(productOverview);
                }



                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateProductBadgeAsync(UpdateProductBadgeInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProductBadge(input);

            if (validationResult.IsSuccess)
            {
                Product product = await _repository.GetAsync(input.ProductId);

                product.PercentageOff = input.PercentageOff;

                await _repository.UpdateAsync(product);

                ProductBadge badge = await _badgeRepository.GetByProduct(product);

                if (badge is null)
                {
                    badge = new()
                    {
                        ProductId = input.ProductId,
                        Badges = input.Badges
                    };

                    await _badgeRepository.CreateAsync(badge);
                }
                else
                {
                    badge.Badges = input.Badges;
                    await _badgeRepository.UpdateAsync(badge);
                }

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }

        }

        public OperationResultEnumerable<ProductDto> GetByCategory(string category)
        {
            return OperationResultEnumerable<ProductDto>.Success(_repository.GetAllProductsByCategory(category).Select(x => x.ConvertToDto()));
        }

        public OperationResultEnumerable<ProductDto> GetByCategory(string category, string subcategory)
        {
            return OperationResultEnumerable<ProductDto>.Success(_repository.GetAllProductsByCategory(category, subcategory).Select(x => x.ConvertToDto()));
        }


    }
}
