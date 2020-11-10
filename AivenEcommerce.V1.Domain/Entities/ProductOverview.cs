using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities.Base;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class ProductOverview : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
    }
}
