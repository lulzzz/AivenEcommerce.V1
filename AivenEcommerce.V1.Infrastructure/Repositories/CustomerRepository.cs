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
    public class CustomerRepository : GitHubRepository<Customer, Guid>, ICustomerRepository
    {
        private readonly ICachedRepository _cachedRepository;

        public CustomerRepository(IGitHubOptions options, IGitHubService githubService, ICachedRepository cachedRepository) : base(githubService, options.CustomerRepositoryId, "customers")
        {
            _cachedRepository = cachedRepository ?? throw new ArgumentNullException(nameof(cachedRepository));
        }

        public async Task<Customer> GetCustomer(string email)
        {
            return await _cachedRepository.GetOrSetAsync(new(nameof(Customer), nameof(GetCustomer), email),

                          async () =>
                          {
                              var fileContent = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, email);

                              if (fileContent is null)
                              {
                                  return null;
                              }

                              return fileContent.Content.Deserialize<Customer>();
                          }
                    );
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
