using AivenEcommerce.V1.Application.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Extensions
{
    public class EnumerableExtensionsShould
    {
        [Theory]
        [InlineData(45)]
        [InlineData("test")]
        public void ConvertToEnumerable_ObjectNotNull_ReturnEnumerableWithOneItem(object obj)
        {
            var enumerable = obj.ConvertToEnumerable();

            Assert.True(enumerable.Count() == 1);
        }

        [Fact]
        public void Add_NewValue_ReturnEnumerableWithOneMoreItem()
        {
            IEnumerable<string> enumerable = Enumerable.Empty<string>();

            enumerable = enumerable.Add("newvalue");

            Assert.True(enumerable.Last() == "newvalue");
        }


        [Fact]
        public void WhereIsNotEmply_ListWithEmptyAndNonEmptyValues_ReturnEnumerableWithOnlyNonEmptyValues()
        {
            IEnumerable<string> enumerable = new List<string>()
            {
                "Value1",
                "Value2",
                "   ",
                string.Empty,
                "Value3"
            };

            enumerable = enumerable.WhereIsNotEmply();

            Assert.DoesNotContain(enumerable, x => string.IsNullOrWhiteSpace(x));
        }
    }
}
