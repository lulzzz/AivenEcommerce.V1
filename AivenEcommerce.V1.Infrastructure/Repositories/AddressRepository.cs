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
        public AddressRepository(IGitHubService githubService, IGitHubOptions options) : base(githubService, options.AddressRepositoryId, "addresses")
        {
        }

        public async Task<AddressCustomer> GetByCustomerAsync(string customer)
        {
            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, customer);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<AddressCustomer>();
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
