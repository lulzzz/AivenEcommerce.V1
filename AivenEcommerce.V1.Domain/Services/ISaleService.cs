using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface ISaleService
    {
        Task<SaleOrderDto> CreateSaleAsync(CreateSaleInput input);
    }
}
