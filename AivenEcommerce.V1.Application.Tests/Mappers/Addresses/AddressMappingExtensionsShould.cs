
using AivenEcommerce.V1.Application.Mappers.Addresses;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;

using System;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Addresses
{
    public class AddressMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameBetweenStreet1()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.BetweenStreet1, addressDto.BetweenStreet1);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameBetweenStreet2()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.BetweenStreet2, addressDto.BetweenStreet2);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameCity()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.City, addressDto.City);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameCustomerEmail()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.CustomerEmail, addressDto.CustomerEmail);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameDepatarment()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Depatarment, addressDto.Depatarment);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameId()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Id, addressDto.Id);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameName()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Name, addressDto.Name);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameNumber()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Number, addressDto.Number);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameObservations()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Observations, addressDto.Observations);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSamePhone()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Phone, addressDto.Phone);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameStreet()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Street, addressDto.Street);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameType()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.Type, addressDto.Type);
        }

        [Fact]
        public void ConvertToDto_AddressNotNull_ReturnSameZipCode()
        {
            Address address = MockAddress();

            AddressDto addressDto = address.ConvertToDto();

            Assert.Equal(address.ZipCode, addressDto.ZipCode);
        }

        private Address MockAddress() =>
            new()
            {
                BetweenStreet1 = nameof(Address.BetweenStreet1),
                BetweenStreet2 = nameof(Address.BetweenStreet2),
                City = nameof(Address.City),
                CustomerEmail = nameof(Address.CustomerEmail),
                Depatarment = nameof(Address.Depatarment),
                Id = Guid.NewGuid(),
                Name = nameof(Address.Name),
                Number = 5,
                Observations = nameof(Address.Observations),
                Phone = nameof(Address.Phone),
                Street = nameof(Address.Street),
                Type = Domain.Shared.Enums.AddressType.Home,
                ZipCode = nameof(Address.ZipCode),

            };
    }
}
