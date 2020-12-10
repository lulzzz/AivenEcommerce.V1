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
    public class CreateCustomerInputMappingExtensionsShould
    {
        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameEmail()
        {
            CreateCustomerInput input = MockCreateCustomerInput();

            Customer customer = input.ConvertToEntity();

            Assert.Equal(input.Email, customer.Email);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameName()
        {
            CreateCustomerInput input = MockCreateCustomerInput();

            Customer customer = input.ConvertToEntity();

            Assert.Equal(input.Name, customer.Name);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSameLastName()
        {
            CreateCustomerInput input = MockCreateCustomerInput();

            Customer customer = input.ConvertToEntity();

            Assert.Equal(input.LastName, customer.LastName);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnSamePicture()
        {
            CreateCustomerInput input = MockCreateCustomerInput();

            Customer customer = input.ConvertToEntity();

            Assert.Equal(input.Picture, customer.Picture);
        }

        [Fact]
        public void ConvertToEntity_InputNotNull_ReturnEmplyId()
        {
            CreateCustomerInput input = MockCreateCustomerInput();

            Customer customer = input.ConvertToEntity();

            Assert.Equal(Guid.Empty, customer.Id);
        }

        private CreateCustomerInput MockCreateCustomerInput() =>
        new(nameof(CreateCustomerInput.Name), nameof(CreateCustomerInput.LastName), nameof(CreateCustomerInput.Email), new("http://contoso.com"));


    }
}

