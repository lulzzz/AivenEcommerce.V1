using AivenEcommerce.V1.Application.Mappers.Addresses;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Addresses
{
    public class UpdateAddressInputMappingExtensionsShould
    {
        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameBetweenStreet1()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.BetweenStreet1, address.BetweenStreet1);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameBetweenStreet2()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.BetweenStreet2, address.BetweenStreet2);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameCity()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.City, address.City);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameCustomerEmail()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.CustomerEmail, address.CustomerEmail);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameDepatarment()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Depatarment, address.Depatarment);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameId()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.AddressId, address.Id);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameName()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Name, address.Name);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameNumber()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Number, address.Number);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameObservations()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Observations, address.Observations);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSamePhone()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Phone, address.Phone);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameStreet()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Street, address.Street);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameType()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.Type, address.Type);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameZipCode()
        {
            UpdateAddressInput input = MockUpdateAddressInput();

            Address address = input.ConvertToEntity();

            Assert.Equal(input.ZipCode, address.ZipCode);
        }

        private UpdateAddressInput MockUpdateAddressInput() =>
            new(Guid.NewGuid(), nameof(Address.Name), nameof(Address.ZipCode), nameof(Address.City), nameof(Address.Street), 5, nameof(Address.Depatarment), nameof(Address.BetweenStreet1), nameof(Address.BetweenStreet2), nameof(Address.Observations), nameof(Address.Phone), Domain.Shared.Enums.AddressType.Home, nameof(Address.CustomerEmail));
    }
}
