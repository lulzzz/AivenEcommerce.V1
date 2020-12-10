using AivenEcommerce.V1.Application.Mappers.CouponCodes;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.CouponCodes;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Mappers.CouponCodes
{
    public class CreateCouponCodeInputMapiingExtensionsShould
    {
        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameCategories()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.Categories, couponCode.Categories);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameCode()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.Code.ToUpper(), couponCode.Code);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameCustomers()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.Customers, couponCode.Customers);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameDateExpire()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.DateExpire, couponCode.DateExpire);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameCDateStart()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.DateStart, couponCode.DateStart);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnEmplyId()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(Guid.Empty, couponCode.Id);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameMaxAmount()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.MaxAmount, couponCode.MaxAmount);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameMinAmount()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.MinAmount, couponCode.MinAmount);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameProducts()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.Products, couponCode.Products);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameSubCategories()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.SubCategories, couponCode.SubCategories);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameType()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.Type, couponCode.Type);
        }

        [Fact]
        public void ConvertToEntity_CouponCodeNotNull_ReturnSameValue()
        {
            CreateCouponCodeInput input = MockCreateCouponCodeInput();

            CouponCode couponCode = input.ConvertToEntity();

            Assert.Equal(input.Value, couponCode.Value);
        }

        private CreateCouponCodeInput MockCreateCouponCodeInput() =>
            new(
                nameof(CreateCouponCodeInput.Code),
                Domain.Shared.Enums.CouponCodeOffType.SpecificAmount,
                20,
                10,
                5000,
                new[] { "a", "b" },
                new[] { new ProductCategoryPair(nameof(ProductCategoryPair.Category), nameof(ProductCategoryPair.SubCategory)) },
                new[] { "e", "f" },
                new[] { "c", "d" },

                DateTime.Now,
                DateTime.Now

            );
    }
}
