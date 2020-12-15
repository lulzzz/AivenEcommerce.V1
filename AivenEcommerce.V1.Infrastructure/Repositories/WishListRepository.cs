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
    public class WishListRepository : GitHubRepository<WishList, Guid>, IWishListRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public WishListRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.WishListRepositoryId, "wishlists")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<WishList> GetByCustomerAsync(string customerEmail) =>

            await _cachedRepository.GetOrSetAsync(new(nameof(Basket), nameof(GetByCustomerAsync), customerEmail),

                       async () =>
                       {
                           var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, customerEmail);

                           if (fileContent is null)
                           {
                               return null;
                           }

                           return fileContent.Content.Deserialize<WishList>();
                       }
                );

        public override async Task<WishList> CreateAsync(WishList entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.CustomerEmail, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(WishList entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.CustomerEmail, entityIn.Serialize());
        }

        public override Task RemoveAsync(WishList entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.CustomerEmail);
        }
    }
}
