using AivenEcommerce.V1.Application.Mappers.Addresses;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Addresses
{
    public class CreateAddressInputMappingExtensionsShould
    {
        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameBetweenStreet1()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.BetweenStreet1, address.BetweenStreet1);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameBetweenStreet2()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.BetweenStreet2, address.BetweenStreet2);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameCity()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.City, address.City);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameCustomerEmail()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.CustomerEmail, address.CustomerEmail);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameDepatarment()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Depatarment, address.Depatarment);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnEmplyId()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(Guid.Empty, address.Id);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameName()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Name, address.Name);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameNumber()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Number, address.Number);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameObservations()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Observations, address.Observations);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSamePhone()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Phone, address.Phone);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameStreet()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Street, address.Street);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameType()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.Type, address.Type);
        }

        [Fact]
        public void ConvertToEntity_AddressNotNull_ReturnSameZipCode()
        {
            CreateAddressInput createAddressInput = MockCreateAddressInput();

            Address address = createAddressInput.ConvertToEntity();

            Assert.Equal(createAddressInput.ZipCode, address.ZipCode);
        }

        private CreateAddressInput MockCreateAddressInput() =>
            new(nameof(Address.Name), nameof(Address.ZipCode), nameof(Address.City), nameof(Address.Street), 5, nameof(Address.Depatarment), nameof(Address.BetweenStreet1), nameof(Address.BetweenStreet2), nameof(Address.Observations), nameof(Address.Phone), Domain.Shared.Enums.AddressType.Home, nameof(Address.CustomerEmail));
    }
}
