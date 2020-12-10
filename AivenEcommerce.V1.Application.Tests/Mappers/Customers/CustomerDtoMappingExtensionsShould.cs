using AivenEcommerce.V1.Application.Mappers.Customers;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Customers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.Customers
{
    public class CustomerDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_InputNotNull_ReturnSameId()
        {
            Customer customer = MockCustomer();

            CustomerDto customerDto = customer.ConvertToDto();

            Assert.Equal(customer.Id, customerDto.Id);
        }

        [Fact]
        public void ConvertToDto_InputNotNull_ReturnSameName()
        {
            Customer customer = MockCustomer();

            CustomerDto customerDto = customer.ConvertToDto();

            Assert.Equal(customer.Name, customerDto.Name);
        }

        [Fact]
        public void ConvertToDto_InputNotNull_ReturnSameLastName()
        {
            Customer customer = MockCustomer();

            CustomerDto customerDto = customer.ConvertToDto();

            Assert.Equal(customer.LastName, customerDto.LastName);
        }

        [Fact]
        public void ConvertToDto_InputNotNull_ReturnSameEmail()
        {
            Customer customer = MockCustomer();

            CustomerDto customerDto = customer.ConvertToDto();

            Assert.Equal(customer.Email, customerDto.Email);
        }

        [Fact]
        public void ConvertToDto_InputNotNull_ReturnSamePicture()
        {
            Customer customer = MockCustomer();

            CustomerDto customerDto = customer.ConvertToDto();

            Assert.Equal(customer.Picture, customerDto.Picture);
        }

        private Customer MockCustomer() =>
        new()
        {
            Name = nameof(Customer.Name),
            LastName = nameof(Customer.LastName),
            Email = nameof(Customer.Email),
            Picture = new("http://contoso.com"),
            Id = Guid.NewGuid()
        };
    }
}
