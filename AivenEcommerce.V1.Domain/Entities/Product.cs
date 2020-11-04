using System;

using AivenEcommerce.V1.Domain.Enums;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
        public ProductSubCategory SubCategory { get; set; }
        public int Stock { get; set; }
        public Uri Thumbnail { get; set; }
        public bool IsActive { get; set; }

    }
}
