﻿using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IAddressRepository : IRepository<AddressCustomer, Guid>
    {
        Task<AddressCustomer> GetByCustomerAsync(string customer);
    }
}
