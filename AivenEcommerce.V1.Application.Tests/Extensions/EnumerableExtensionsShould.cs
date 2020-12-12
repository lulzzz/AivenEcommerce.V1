using AivenEcommerce.V1.Application.Extensions;

using System.Collections.Generic;
using System.Linq;

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
        public void Add_NewValueInExistentEnumerable_ReturnEnumerableWithOneMoreItem()
        {
            IEnumerable<int> enumerable = new int[] { 1, 2, 3, 4 };

            enumerable = enumerable.Add(5);

            Assert.True(enumerable.Last() == 5);
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
