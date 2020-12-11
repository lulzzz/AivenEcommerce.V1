using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using FluentAssertions;

using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Validators
{
    public class AddressValidatorShould
    {
        #region ValidateCreateAddressAsync

        [Fact]
        public async Task ValidateCreateAddressAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), nameof(CreateAddressInput.ZipCode), nameof(CreateAddressInput.City), nameof(CreateAddressInput.Street), 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_CustomerEmailDontExist_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), nameof(CreateAddressInput.ZipCode), nameof(CreateAddressInput.City), nameof(CreateAddressInput.Street), 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), nameof(CreateAddressInput.ZipCode), nameof(CreateAddressInput.City), nameof(CreateAddressInput.Street), 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, null);

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_NameIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(null, nameof(CreateAddressInput.ZipCode), nameof(CreateAddressInput.City), nameof(CreateAddressInput.Street), 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }


        [Fact]
        public async Task ValidateCreateAddressAsync_ZipCodeIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), null, nameof(CreateAddressInput.City), nameof(CreateAddressInput.Street), 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_CityIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), nameof(CreateAddressInput.ZipCode), null, nameof(CreateAddressInput.Street), 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_StreetIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), nameof(CreateAddressInput.ZipCode), nameof(CreateAddressInput.City), null, 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateCreateAddressAsync_TypeInvalid_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            CreateAddressInput input = new(nameof(CreateAddressInput.Name), nameof(CreateAddressInput.ZipCode), nameof(CreateAddressInput.City), null, 1, nameof(CreateAddressInput.Depatarment), nameof(CreateAddressInput.BetweenStreet1), nameof(CreateAddressInput.BetweenStreet2), nameof(CreateAddressInput.Observations), nameof(CreateAddressInput.Phone), (Domain.Shared.Enums.AddressType)3, nameof(CreateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCreateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        #endregion

        #region ValidateUpdateAddressAsync

        [Fact]
        public async Task ValidateUpdateAddressAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();
            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            UpdateAddressInput input = new(addressId, nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_CustomerEmailDontExist_ReturnValidationResultFail()
        {
            MockObject mockObject = new();
            Guid addressId = Guid.NewGuid();
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);

            UpdateAddressInput input = new(addressId, nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, null);

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_NameIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            UpdateAddressInput input = new(Guid.NewGuid(), null, nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }


        [Fact]
        public async Task ValidateUpdateAddressAsync_ZipCodeIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), null, nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_CityIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), null, nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_StreetIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), null, 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_TypeInvalid_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), null, 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), (Domain.Shared.Enums.AddressType)3, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_AddressNonExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = Guid.NewGuid() } }
            });

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_CustomerHaventAddresses_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new List<Address>()
            });

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_AddressCustomerIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateUpdateAddressAsync_CustomerHaventAddressesNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = null
            });

            UpdateAddressInput input = new(Guid.NewGuid(), nameof(UpdateAddressInput.Name), nameof(UpdateAddressInput.ZipCode), nameof(UpdateAddressInput.City), nameof(UpdateAddressInput.Street), 1, nameof(UpdateAddressInput.Depatarment), nameof(UpdateAddressInput.BetweenStreet1), nameof(UpdateAddressInput.BetweenStreet2), nameof(UpdateAddressInput.Observations), nameof(UpdateAddressInput.Phone), Domain.Shared.Enums.AddressType.Home, nameof(UpdateAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateUpdateAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        #endregion

        #region ValidateDeleteAddressAsync

        [Fact]
        public async Task ValidateDeleteAddressAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();
            Guid addressId = Guid.NewGuid();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            DeleteAddressInput input = new(addressId, nameof(DeleteAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ValidateDeleteAddressAsync_CustomerEmailDontExist_ReturnValidationResultFail()
        {
            MockObject mockObject = new();
            Guid addressId = Guid.NewGuid();
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = addressId } }
            });

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(() => null);

            DeleteAddressInput input = new(addressId, nameof(DeleteAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateDeleteAddressAsync_CustomerEmailIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

     
            DeleteAddressInput input = new(Guid.NewGuid(), null);

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateDeleteAddressAsync_AddressNonExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new[] { new Domain.Entities.Address { Id = Guid.NewGuid() } }
            });

            DeleteAddressInput input = new(Guid.NewGuid(), nameof(DeleteAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateDeleteAddressAsync_CustomerHaventAddresses_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = new List<Address>()
            });

            DeleteAddressInput input = new(Guid.NewGuid(), nameof(DeleteAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateDeleteAddressAsync_AddressCustomerIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            DeleteAddressInput input = new(Guid.NewGuid(), nameof(DeleteAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateDeleteAddressAsync_CustomerHaventAddressesNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.CustomerRepositoryMock.Setup(x => x.GetCustomer(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Customer());
            mockObject.AddressRepositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.AddressCustomer
            {
                Addresses = null
            });

            DeleteAddressInput input = new(Guid.NewGuid(), nameof(DeleteAddressInput.CustomerEmail));

            ValidationResult validationResult = await mockObject.GetValidator().ValidateDeleteAddressAsync(input);

            validationResult.IsSuccess.Should().BeFalse();
        }

        #endregion

        class MockObject
        {
            public MockObject()
            {
                AddressRepositoryMock = new Mock<IAddressRepository>();
                CustomerRepositoryMock = new Mock<ICustomerRepository>();
            }

            public Mock<IAddressRepository> AddressRepositoryMock { get; set; }
            public Mock<ICustomerRepository> CustomerRepositoryMock { get; set; }


            public AddressValidator GetValidator() => new AddressValidator(AddressRepositoryMock.Object, CustomerRepositoryMock.Object);

        }
    }
}
