using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Collections.Generic;
using System.Net;

namespace AivenEcommerce.V1.Domain.Shared.OperationResults
{
    public class OperationResultEnumerable<T> : OperationResult
    {
        public OperationResultEnumerable()
        {
        }

        public OperationResultEnumerable(IEnumerable<T> result)
        {
            Result = result;
            IsSuccess = true;
        }

        public IEnumerable<T> Result { get; set; }

        public static OperationResultEnumerable<T> Success(IEnumerable<T> result)
        {
            return new()
            {
                IsSuccess = true,
                Result = result,
                Status = HttpStatusCode.OK
            };
        }

        public static new OperationResultEnumerable<T> Fail(ValidationResult validations) =>

             new()
             {
                 Validations = validations,
                 Status = HttpStatusCode.BadRequest,
                 IsSuccess = false
             };


        public static new OperationResultEnumerable<T> Error(ValidationResult validations) =>

             new()
             {
                 Validations = validations,
                 Status = HttpStatusCode.InternalServerError,
                 IsSuccess = false
             };

    }
}
