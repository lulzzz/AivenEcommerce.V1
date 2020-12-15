using AivenEcommerce.V1.Domain.Caching;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Infrastructure.Extensions;
using AivenEcommerce.V1.Infrastructure.Repositories.Base;
using AivenEcommerce.V1.Modules.GitHub.Options;
using AivenEcommerce.V1.Modules.GitHub.Services;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Infrastructure.Repositories
{
    public class DeliveryRepository : GitHubRepository<Delivery, Guid>, IDeliveryRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public DeliveryRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.DeliveryRepositoryId, "deliveries")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<Delivery> GetDeliveryAsync(Order order)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(Basket), nameof(GetDeliveryAsync), order.Id),

                          async () =>
                          {

                              var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, order.Id);

                              if (fileContent is null)
                              {
                                  return null;
                              }

                              return fileContent.Content.Deserialize<Delivery>();

                          }
                    );
        }

        public override async Task<Delivery> CreateAsync(Delivery entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.OrderId, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(Delivery entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.OrderId, entityIn.Serialize());
        }

        public override Task RemoveAsync(Delivery entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.OrderId);
        }
    }
}
