using AivenEcommerce.V1.Application.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Extensions
{
    public class StringExtensionsShould
    {
        [Theory]
        [InlineData("test")]
        [InlineData("453")]
        [InlineData("AA;AA")]
        [InlineData("  AA  AA ")]
        [InlineData("")]
        [InlineData("¿Que")]
        [InlineData("'Que'")]
        public void HasFileInvalidChars_StringWithoutInvalidChars_ReturnFalse(string str)
        {
            Assert.False(str.HasFileInvalidChars());
        }

        [Theory]
        [InlineData("<")]
        [InlineData(">")]
        [InlineData("\"")]
        [InlineData("/")]
        [InlineData("\\")]
        [InlineData("|")]
        [InlineData("*")]
        [InlineData("?")]
        public void HasFileInvalidChars_StringWithInvalidChars_ReturnTrue(string str)
        {
            Assert.True(str.HasFileInvalidChars());
        }

        [Theory]
        [InlineData("MyEmail@domain.com")]
        [InlineData("MyEmail@domain.com.ar")]
        [InlineData("name.lastname@domain.com.ar")]
        public void IsEmail_EmailValid_ReturnTrue(string str)
        {
            Assert.True(str.IsEmail());
        }

        [Theory]
        [InlineData("MyEmail")]
        [InlineData("MyEmail.com")]
        [InlineData("MyEmail@domain")]
        [InlineData("MyEmail.com@domain")]
        [InlineData("@domain")]
        [InlineData("domain.com")]
        [InlineData("domain.com.ar")]
        public void IsEmail_EmailInvalid_ReturnFalse(string str)
        {
            Assert.False(str.IsEmail());
        }
    }
}
