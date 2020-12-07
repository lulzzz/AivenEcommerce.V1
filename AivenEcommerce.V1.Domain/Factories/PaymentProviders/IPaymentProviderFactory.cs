using AivenEcommerce.V1.Domain.Shared.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Factories.PaymentProviders
{
    public interface IPaymentProviderFactory
    {
        IPaymentProvider this[PaymentProvider provider] { get; }

        PaymentProvider[] RegisteredTypes { get; }

        IPaymentProvider CreatePaymentProvider(PaymentProvider provider);
        void RegisterPaymentProvider(PaymentProvider providerType, Func<IPaymentProvider> factoryMethod);
    }
}