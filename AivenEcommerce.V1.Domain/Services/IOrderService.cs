using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Shared.Paginations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IOrderService : IScopedService
    {
        Task<OperationResult<PagedResult<OrderDto>>> GetAllAsync(OrderParameters parameters);
        Task<OperationResult<OrderDto>> CancelOrderAsync(CancelOrderInput input);
    }
}
