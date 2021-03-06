﻿using AivenEcommerce.V1.Modules.PayPal.Enum;

namespace AivenEcommerce.V1.Modules.PayPal.Options
{
    public interface IPayPalOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public PayPalEnvironment Environment { get; set; }
    }
}
