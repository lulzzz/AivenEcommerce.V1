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
    public class CustomerRepository : GitHubRepository<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(IGitHubOptions options, IGitHubService githubService) : base(githubService, options.CustomerRepositoryId, "customers")
        {

        }

        public async Task<Customer> GetCustomer(string email)
        {
            var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, email);

            if (fileContent is null)
            {
                return null;
            }

            return fileContent.Content.Deserialize<Customer>();
        }

        public override async Task<Customer> CreateAsync(Customer entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.Email, entity.Serialize());

            return entity;
        }

        public override Task UpdateAsync(Customer entityIn)
        {
            return base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.Email, entityIn.Serialize());
        }

        public override Task RemoveAsync(Customer entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.Email);
        }
    }
}
