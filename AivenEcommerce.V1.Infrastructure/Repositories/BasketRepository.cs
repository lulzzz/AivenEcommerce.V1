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
    public class BasketRepository : GitHubRepository<Basket, Guid>, IBasketRepository
    {
        public BasketRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.BasketRepositoryId, "baskets")
        {
        }

        public async Task<Basket> GetByCustomerAsync(string customerEmail)
        {
            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, customerEmail);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<Basket>();
        }

        public override async Task<Basket> CreateAsync(Basket entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.CustomerEmail, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(Basket entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.CustomerEmail, entityIn.Serialize());
        }

        public override Task RemoveAsync(Basket entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.CustomerEmail);
        }
    }
}
