﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Dtos.ProductCategories
{
    public record DeleteProductSubCategoryInput(string CategoryName, string SubCategoryName);
}
