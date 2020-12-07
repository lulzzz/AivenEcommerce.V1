using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Shared.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AivenEcommerce.V1.Application.Factories.PaymentProviders
{
    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        private readonly Dictionary<PaymentProvider, Func<IPaymentProvider>> paymentProviders;

        public PaymentProviderFactory()
        {
            paymentProviders = new();
        }

        public IPaymentProvider this[PaymentProvider provider] => CreatePaymentProvider(provider);

        public IPaymentProvider CreatePaymentProvider(PaymentProvider provider) => paymentProviders[provider]();

        public PaymentProvider[] RegisteredTypes => paymentProviders.Keys.ToArray();

        public void RegisterPaymentProvider(PaymentProvider providerType, Func<IPaymentProvider> factoryMethod)
        {
            paymentProviders[providerType] = factoryMethod;
        }
    }
}
