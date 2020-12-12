using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using FluentAssertions;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Validators
{
    public class SaleValidatorShould
    {
        [Fact]
        public async Task CreateSaleAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeSuccess();
        }

        [Fact]
        public async Task CreateSaleAsync_TypeInvalid_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), (PaymentProvider)6);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(null, nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => new());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_CustomerEmailDontExist_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_AddressNonExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = Guid.NewGuid() } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_CustomerHaventAddresses_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new List<Address>()
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_CustomerHaventAddressesNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = null
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_CouponCodeDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_ProductsIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), null, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_ProductDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_PRoductIsNotActive_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = false,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_ProductHaventStock_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 0
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_ProductVariantNameDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "NameFake",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task CreateSaleAsync_ProductVariantValueDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            CreateSaleInput input = new(nameof(CreateSaleInput.CustomerEmail), nameof(CreateSaleInput.CouponCode), new List<ProductDefinitive>
            {
                new("1", new List<ProductVariantPair>
                {
                    new("Name", "Value")
                }, 2)
            }, Guid.NewGuid(), PaymentProvider.MercadoPago);

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = input.AddressId } }
            });

            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Product
            {
                IsActive = true,
                Stock = 10
            });
            mockObject.CouponCodeRepositoryMock.Setup(x => x.GetCouponAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.CouponCode());

            mockObject.ProductVariantRepository.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "ValueFake" }
                }
            });

            ValidationResult validationResult = await mockObject.GetValidator().CreateSaleAsync(input);

            validationResult.Should().BeFail();
        }


        class MockObject
        {
            public MockObject()
            {
                AddressRepositoryMock = new Mock<IAddressRepository>();
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
                CouponCodeRepositoryMock = new Mock<ICouponCodeRepository>();
                ProductRepositoryMock = new Mock<IProductRepository>();
                ProductVariantRepository = new Mock<IProductVariantRepository>();
            }

            public Mock<IAddressRepository> AddressRepositoryMock { get; set; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; set; }
            public Mock<ICouponCodeRepository> CouponCodeRepositoryMock { get; set; }
            public Mock<IProductRepository> ProductRepositoryMock { get; set; }
            public Mock<IProductVariantRepository> ProductVariantRepository { get; set; }


            public SaleValidator GetValidator() => new(CustomerRepositoryMock.Object, CouponCodeRepositoryMock.Object, ProductRepositoryMock.Object, ProductVariantRepository.Object, AddressRepositoryMock.Object);

        }

    }
}
