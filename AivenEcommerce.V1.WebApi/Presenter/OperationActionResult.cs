using AivenEcommerce.V1.Domain.Shared.OperationResults;

using Microsoft.AspNetCore.Mvc;

namespace AivenEcommerce.V1.WebApi.Presenter
{
    public class OperationActionResult : ObjectResult
    {
        public OperationActionResult(OperationResult value) : base(value)
        {
            StatusCode = (int)value.Status;
        }
    }
}
