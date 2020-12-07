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
        public DeliveryRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.DeliveryRepositoryId, "deliveries")
        {
        }

        public async Task<Delivery> GetDeliveryAsync(Order order)
        {
            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, order.Id);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<Delivery>();
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
