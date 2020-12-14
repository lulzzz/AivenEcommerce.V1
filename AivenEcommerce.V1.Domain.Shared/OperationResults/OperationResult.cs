using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Net;

namespace AivenEcommerce.V1.Domain.Shared.OperationResults
{
    public class OperationResult
    {
        public HttpStatusCode Status { get; set; }

        public bool IsSuccess { get; set; }

        public ValidationResult Validations { get; set; }

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

             new()
             {
                 Validations = validations,
                 Status = HttpStatusCode.InternalServerError,
                 IsSuccess = false
             };

    }
}
