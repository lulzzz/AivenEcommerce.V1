using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using FluentAssertions;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Validators
{
    public class ProductImageValidatorShould
    {
        [Fact]
        public async Task ValidateDeleteProductImage_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            DeleteProductImageInput input = new("1", Guid.NewGuid());

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());
            mockObject.ProductImageRepositoryMock.Setup(x => x.GetProductImages(It.IsAny<Product>())).ReturnsAsync(new List<ProductImage>
            {
                new ProductImage
                {
                    Id = input.ProductImageId,
                    Image = new("https://contoso.com")
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteProductImage(input);

            validationResult.Should().BeSuccess();
        }

        [Fact]
        public async Task ValidateDeleteProductImage_ProductIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            DeleteProductImageInput input = new(null, Guid.NewGuid());

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());
            mockObject.ProductImageRepositoryMock.Setup(x => x.GetProductImages(It.IsAny<Product>())).ReturnsAsync(new List<ProductImage>
            {
                new ProductImage
                {
                    Id = input.ProductImageId,
                    Image = new("https://contoso.com")
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteProductImage(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateDeleteProductImage_ProductDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            DeleteProductImageInput input = new("1", Guid.NewGuid());

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.ProductImageRepositoryMock.Setup(x => x.GetProductImages(It.IsAny<Product>())).ReturnsAsync(new List<ProductImage>
            {
                new ProductImage
                {
                    Id = input.ProductImageId,
                    Image = new("https://contoso.com")
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteProductImage(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateDeleteProductImage_ProductImageDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            DeleteProductImageInput input = new("1", Guid.NewGuid());

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());
            mockObject.ProductImageRepositoryMock.Setup(x => x.GetProductImages(It.IsAny<Product>())).ReturnsAsync(new List<ProductImage>());

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteProductImage(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateDeleteProductImage_ProductImageIsPrincipalProductThumbnail_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            DeleteProductImageInput input = new("1", Guid.NewGuid());

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                Thumbnail = new("https://contoso.com")
            });
            mockObject.ProductImageRepositoryMock.Setup(x => x.GetProductImages(It.IsAny<Product>())).ReturnsAsync(new List<ProductImage>
            {
                new ProductImage
                {
                    Id = input.ProductImageId,
                    Image = new("https://contoso.com")
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteProductImage(input);

            validationResult.Should().BeFail();
        }

        class MockObject
        {
            public MockObject()
            {
                ProductRepositoryMock = new Mock<IProductRepository>();
                ProductImageRepositoryMock = new Mock<IProductImageRepository>();
            }

            public Mock<IProductRepository> ProductRepositoryMock { get; set; }
            public Mock<IProductImageRepository> ProductImageRepositoryMock { get; set; }


            public ProductImageValidator GetValidator() => new(ProductRepositoryMock.Object, ProductImageRepositoryMock.Object);

        }
    }
}
