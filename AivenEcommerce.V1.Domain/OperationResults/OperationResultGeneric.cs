using System.Net;

using AivenEcommerce.V1.Application.Validations;

namespace BusinessLogicEnterprise.Application.OperationResults
{
    public class OperationResult<T> : OperationResult where T : class
    {
        public OperationResult()
        {
        }

        public OperationResult(T result)
        {
            Result = result;
            IsSuccess = true;
        }

        public T? Result { get; set; }

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>
            {
                IsSuccess = true,
                Result = result,
                Status = HttpStatusCode.OK
            };
        }


        public static OperationResult<T> Fail(string message, T result) =>

            new OperationResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Message = message,
                Result = result
            };

        public static OperationResult<T> Fail(string message) =>

            new OperationResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Message = message,
                Result = null
            };

        public static OperationResult<T> Error(string message, T result) =>

            new OperationResult<T>
            {
                Status = HttpStatusCode.InternalServerError,
                Message = message,
                Result = result
            };

        public static OperationResult<T> Error(string message) =>

            new OperationResult<T>
            {
                Status = HttpStatusCode.InternalServerError,
                Message = message,
                Result = null
            };

        public static OperationResult<T> Fail(ValidationResult validations) =>

             new OperationResult<T>
             {
                 Validations = validations,
                 Status = HttpStatusCode.BadRequest,
                 IsSuccess = false
             };


        public static OperationResult<T> NotFound(ValidationResult validations) =>

             new OperationResult<T>
             {
                 Validations = validations,
                 Status = HttpStatusCode.NotFound,
                 IsSuccess = false
             };

        public static OperationResult<T> NotFound() =>

             new OperationResult<T>
             {
                 Status = HttpStatusCode.NotFound,
                 IsSuccess = false
             };
    }
}
