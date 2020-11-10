using System.Net;

using AivenEcommerce.V1.Application.Validations;

namespace AivenEcommerce.V1.Domain.OperationResults
{
    public class OperationResult
    {
        public HttpStatusCode Status { get; protected set; }

        public bool IsSuccess { get; protected set; }

        public string? Message { get; protected set; }

        public ValidationResult? Validations { get; protected set; }

        public static OperationResult Success() =>

            new OperationResult
            {
                Status = HttpStatusCode.OK,
                IsSuccess = true
            };


        public static OperationResult Fail() =>

             new OperationResult
             {
                 Status = HttpStatusCode.BadRequest,
                 IsSuccess = false
             };


        public static OperationResult Error() =>

             new OperationResult
             {
                 Status = HttpStatusCode.InternalServerError,
                 IsSuccess = false
             };

        public static OperationResult Fail(ValidationResult validations) =>

             new OperationResult
             {
                 Validations = validations,
                 Status = HttpStatusCode.BadRequest,
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
