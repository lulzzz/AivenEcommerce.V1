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
    public class CouponCodeDtoMappingExtensionsShould
    {
        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameCategories()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Categories, couponCodeDto.Categories);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameCode()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Code, couponCodeDto.Code);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameCustomers()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Customers, couponCodeDto.Customers);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameDateExpire()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.DateExpire, couponCodeDto.DateExpire);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameCDateStart()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.DateStart, couponCodeDto.DateStart);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameId()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Id, couponCodeDto.Id);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameMaxAmount()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.MaxAmount, couponCodeDto.MaxAmount);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameMinAmount()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.MinAmount, couponCodeDto.MinAmount);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameProducts()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Products, couponCodeDto.Products);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameSubCategories()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.SubCategories, couponCodeDto.SubCategories);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameType()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Type, couponCodeDto.Type);
        }

        [Fact]
        public void ConvertToDto_CouponCodeNotNull_ReturnSameValue()
        {
            CouponCode couponCode = MockCouponCode();

            CouponCodeDto couponCodeDto = couponCode.ConvertToDto();

            Assert.Equal(couponCode.Value, couponCodeDto.Value);
        }

        private CouponCode MockCouponCode() =>
        new()
        {
            Categories = new[] {"a", "b" },
            Code = nameof(CouponCode.Code),
            Customers = new[] { "c", "d" },
            DateExpire = DateTime.Now,
            DateStart = DateTime.Now,
            Id = Guid.NewGuid(),
            MaxAmount = 5000,
            MinAmount = 10,
            Products = new[] { "e", "f" },
            Type = Domain.Shared.Enums.CouponCodeOffType.SpecificAmount,
            Value = 20,
            SubCategories = new[] { new ProductCategoryPair(nameof(ProductCategoryPair.Category), nameof(ProductCategoryPair.SubCategory)) }
        };
    }
}
