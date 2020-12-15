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
    public class AddressRepository : GitHubRepository<AddressCustomer, Guid>, IAddressRepository
    {
        private readonly ICachedRepository _cachedRepository;
        public AddressRepository(IGitHubService githubService, IGitHubOptions options, ICachedRepository cachedRepository) : base(githubService, options.AddressRepositoryId, "addresses")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<AddressCustomer> GetByCustomerAsync(string customer)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(AddressCustomer), nameof(GetByCustomerAsync), customer), async () =>
            {

                var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, customer);

                if (fileContent is null)
                {
                    return null;
                }

                return fileContent.Content.Deserialize<AddressCustomer>();

            });
        }

        public override async Task<AddressCustomer> CreateAsync(AddressCustomer entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.CustomerEmail, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(AddressCustomer entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.CustomerEmail, entityIn.Serialize());
        }

        public override Task RemoveAsync(AddressCustomer entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.CustomerEmail);
        }
    }
}
