using AivenEcommerce.V1.Application.Validations;

using System.Net;

namespace AivenEcommerce.V1.Domain.OperationResults
{
    public class OperationResult
    {
        public HttpStatusCode Status { get; protected set; }

        public bool IsSuccess { get; protected set; }

        public ValidationResult Validations { get; protected set; }

        public static OperationResult Success() =>

            new()
            {
                Status = HttpStatusCode.OK,
                IsSuccess = true
            };


        public static OperationResult Fail() =>

             new()
             {
                 Status = HttpStatusCode.BadRequest,
                 IsSuccess = false
             };

        public static OperationResult Fail(ValidationResult validations) =>

             new()
             {
                 Validations = validations,
                 Status = HttpStatusCode.BadRequest,
                 IsSuccess = false
             };

        public static OperationResult Error() =>

             new()
             {
                 Status = HttpStatusCode.InternalServerError,
                 IsSuccess = false
             };

        public static OperationResult Error(ValidationResult validations) =>

             new OperationResult
             {
                 Validations = validations,
                 Status = HttpStatusCode.InternalServerError,
                 IsSuccess = false
             };

    }
}
