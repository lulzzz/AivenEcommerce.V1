using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface ICouponCodeRepository : IRepository<CouponCode, Guid>
    {
        Task<CouponCode> GetCouponAsync(string code);
    }
}
