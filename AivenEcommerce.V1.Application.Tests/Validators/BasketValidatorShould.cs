using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Validators
{
    public class BasketValidatorShould
    {

        #region ValidateCreateAddressAsync

        [Fact]
        public async Task ValidateCreateAddressAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeSuccess();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_ProductIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(null, "user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_ProductDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_ProductIsInactive_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = false
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_ProductQuantityIsZero_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                0), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_CustomerEmailDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5), null);


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_ProductVariantNameDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("NameFake", "Value")
                },
                5), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_ProductVariantValueDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            AddBasketProductInput input = new(new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "ValueFake")
                },
                5), "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateAddBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        #endregion

        #region ValidateUpdateBasketAsync

        [Fact]
        public async Task ValidateUpdateBasketAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeSuccess();
        }


        [Fact]
        public async Task ValidateUpdateBasketAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5) }, null);


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateUpdateBasketAsync_CustomerDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateUpdateBasketAsync_ProductDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateUpdateBasketAsync_ProductQuantityIsZero_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                0) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateUpdateBasketAsync_ProductVariantNameDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("NameFake", "Value")
                },
                5) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateUpdateBasketAsync_ProductVariantValueDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = true
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "ValueFake")
                },
                5) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateUpdateBasketAsync_ProductIsInactive_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product
            {
                IsActive = false
            });

            mockObject.ProductVariantRepositoryMock.Setup(x => x.GetByProduct(It.IsAny<Product>())).ReturnsAsync(new List<ProductVariant>
            {
                new ProductVariant
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    ProductId = "1",
                    Values = new [] { "Value" }
                }
            });

            UpdateBasketInput input = new(new[] {new Domain.Shared.Dtos.Products.ProductDefinitive("1", new List<ProductVariantPair>
                {
                    new ProductVariantPair("Name", "Value")
                },
                5) }, "user@domain.com");


            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateBasketAsync(input);

            validationResult.Should().BeFail();
        }


        #endregion

        #region ValidateRemoveAllBasketAsync

        [Fact]
        public async Task ValidateRemoveAllBasketAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket());

            RemoveAllBasketInput input = new("user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveAllBasketAsync(input);

            validationResult.Should().BeSuccess();
        }

        [Fact]
        public async Task ValidateRemoveAllBasketAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket());

            RemoveAllBasketInput input = new(null);

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveAllBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateRemoveAllBasketAsync_CustomerDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket());

            RemoveAllBasketInput input = new("user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveAllBasketAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateRemoveAllBasketAsync_BasketDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            RemoveAllBasketInput input = new("user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveAllBasketAsync(input);

            validationResult.Should().BeFail();
        }

        #endregion

        #region ValidateRemoveBasketProductAsync

        [Fact]
        public async Task ValidateRemoveBasketProductAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket
            {
                Products = new List<ProductDefinitive>
                {
                    new("1", new List<ProductVariantPair>(), 4)
                }
            });
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());

            RemoveBasketProductInput input = new("1", "user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveBasketProductAsync(input);

            validationResult.Should().BeSuccess();
        }

        [Fact]
        public async Task ValidateRemoveBasketProductAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket
            {
                Products = new List<ProductDefinitive>
                {
                    new("1", new List<ProductVariantPair>(), 4)
                }
            });
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());

            RemoveBasketProductInput input = new("1", null);

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateRemoveBasketProductAsync_CustomerDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket
            {
                Products = new List<ProductDefinitive>
                {
                    new("1", new List<ProductVariantPair>(), 4)
                }
            });
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());

            RemoveBasketProductInput input = new("1", "user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateRemoveBasketProductAsync_BasketDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());

            RemoveBasketProductInput input = new("1", "user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateRemoveBasketProductAsync_ProductDontExistsInBasket_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Customer());
            mockObject.BasketRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Basket
            {
                Products = new List<ProductDefinitive>
                {

                }
            });
            mockObject.ProductRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Product());

            RemoveBasketProductInput input = new("1", "user@domain.com");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateRemoveBasketProductAsync(input);

            validationResult.Should().BeFail();
        }

        #endregion

        class MockObject
        {
            public MockObject()
            {
                BasketRepositoryMock = new Mock<IBasketRepository>();
                ProductRepositoryMock = new Mock<IProductRepository>();
                ProductVariantRepositoryMock = new Mock<IProductVariantRepository>();
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
            }

            public Mock<IBasketRepository> BasketRepositoryMock { get; set; }
            public Mock<IProductRepository> ProductRepositoryMock { get; set; }
            public Mock<IProductVariantRepository> ProductVariantRepositoryMock { get; set; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; set; }


            public BasketValidator GetValidator() => new(BasketRepositoryMock.Object, ProductRepositoryMock.Object, ProductVariantRepositoryMock.Object, CustomerRepositoryMock.Object);

        }
    }
}
