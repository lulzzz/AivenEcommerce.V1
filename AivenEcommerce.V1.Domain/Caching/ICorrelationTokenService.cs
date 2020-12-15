using AivenEcommerce.V1.Domain.Shared.Common;

using System.Threading;

namespace AivenEcommerce.V1.Domain.Caching
{
    public interface ICorrelationTokenService : ISingletonService
    {
        CancellationTokenSource GetOrCreate();
        void Remove();
    }
}
