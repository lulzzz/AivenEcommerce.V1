using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface ICouponCodeRepository : IRepository<CouponCode, Guid>
    {
        Task<CouponCode> GetCouponAsync(string code);
    }
}
