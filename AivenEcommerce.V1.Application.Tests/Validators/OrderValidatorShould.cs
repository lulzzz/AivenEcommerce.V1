using AivenEcommerce.V1.Application.Validators;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using Moq;

using System.Threading.Tasks;

using Xunit;

namespace AivenEcommerce.V1.Application.Tests.Validators
{
    public class OrderValidatorShould
    {
        [Fact]
        public async Task ValidateCancelOrderAsync_AllFieldHasValidValues_ReturnValidationResultSuccess()
        {
            MockObject mockObject = new();

            mockObject.OrderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Order());

            CancelOrderInput input = new("1");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCancelOrderAsync(input);

            validationResult.Should().BeSuccess();
        }

        [Fact]
        public async Task ValidateCancelOrderAsync_OrderIdIsNull_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.OrderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Order());

            CancelOrderInput input = new(null);

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCancelOrderAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCancelOrderAsync_OrderDontExists_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.OrderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            CancelOrderInput input = new("1");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCancelOrderAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCancelOrderAsync_OrderIsPayed_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.OrderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Order
            {
                Status = Domain.Shared.Enums.OrderStatus.Payed
            });

            CancelOrderInput input = new("1");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCancelOrderAsync(input);

            validationResult.Should().BeFail();
        }

        [Fact]
        public async Task ValidateCancelOrderAsync_OrderIsCanceled_ReturnValidationResultFail()
        {
            MockObject mockObject = new();

            mockObject.OrderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new Domain.Entities.Order
            {
                Status = Domain.Shared.Enums.OrderStatus.Canceled
            });

            CancelOrderInput input = new("1");

            ValidationResult validationResult = await mockObject.GetValidator().ValidateCancelOrderAsync(input);

            validationResult.Should().BeFail();
        }

        class MockObject
        {
            public MockObject()
            {
                OrderRepositoryMock = new Mock<IOrderRepository>();
            }

            public Mock<IOrderRepository> OrderRepositoryMock { get; set; }

            public OrderValidator GetValidator() => new(OrderRepositoryMock.Object);

        }
    }
}
