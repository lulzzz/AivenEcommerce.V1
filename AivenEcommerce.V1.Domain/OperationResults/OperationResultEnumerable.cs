using System.Collections.Generic;
using System.Net;

using AivenEcommerce.V1.Application.Validations;

namespace AivenEcommerce.V1.Domain.OperationResults
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
            return new OperationResultEnumerable<T>
            {
                IsSuccess = true,
                Result = result,
                Status = HttpStatusCode.OK
            };
        }

        public static new OperationResultEnumerable<T> Success() => Success(default);


        public static OperationResultEnumerable<T> Fail(string message, IEnumerable<T> result) =>

            new OperationResultEnumerable<T>
            {
                Status = HttpStatusCode.BadRequest,
                Message = message,
                Result = result
            };

        public static OperationResultEnumerable<T> Fail(string message) => Fail(message, null);

        public static OperationResultEnumerable<T> Error(string message, IEnumerable<T> result) =>

            new OperationResultEnumerable<T>
            {
                Status = HttpStatusCode.InternalServerError,
                Message = message,
                Result = result
            };

        public static OperationResultEnumerable<T> Error(string message) => Error(message, null);

        public static OperationResultEnumerable<T> Fail(ValidationResult validations) =>

             new OperationResultEnumerable<T>
             {
                 Validations = validations,
                 Status = HttpStatusCode.BadRequest,
                 IsSuccess = false
             };


        public static OperationResultEnumerable<T> Error(ValidationResult validations) =>

             new OperationResultEnumerable<T>
             {
                 Validations = validations,
                 Status = HttpStatusCode.InternalServerError,
                 IsSuccess = false
             };

    }
}
