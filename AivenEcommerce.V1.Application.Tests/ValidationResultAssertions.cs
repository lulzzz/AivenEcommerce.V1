using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace AivenEcommerce.V1.Application.Tests
{

    public static class ValidationResultExtensions
    {
        public static ValidationResultAssertions Should(this ValidationResult instance)
        {
            return new ValidationResultAssertions(instance);
        }
    }

    public class ValidationResultAssertions : ReferenceTypeAssertions<ValidationResult, ValidationResultAssertions>
    {
        private readonly ValidationResult _validationResult;

        public ValidationResultAssertions(ValidationResult validationResult)
        {
            _validationResult = validationResult;
        }

        protected override string Identifier => "validationresult";

        public AndConstraint<ValidationResultAssertions> BeFail(string because = "", params object[] becauseArgs)
        {
            _validationResult.IsSuccess.Should().BeFalse(because, becauseArgs);
            _validationResult.Messages.Should().HaveCount(1);

            return new AndConstraint<ValidationResultAssertions>(this);
        }

        public AndConstraint<ValidationResultAssertions> BeSuccess(string because = "", params object[] becauseArgs)
        {
            _validationResult.IsSuccess.Should().BeTrue(because, becauseArgs);

            return new AndConstraint<ValidationResultAssertions>(this);
        }
    }
}
