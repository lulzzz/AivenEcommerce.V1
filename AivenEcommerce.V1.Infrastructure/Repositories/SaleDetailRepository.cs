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
    public class SaleDetailRepository : GitHubRepository<SaleDetail, Guid>, ISaleDetailRepository
    {
        public SaleDetailRepository(IGitHubOptions options, IGitHubService githubService) : base(githubService, options.SaleDetailRepositoryId, "saledetails")
        {

        }

        public async Task<SaleDetail> GetBySale(Sale sale)
        {
            var file = await base.GithubService.GetFileContentAsync(base.RepositoryId, base.Path, sale.Id);
            return file.Content.Deserialize<SaleDetail>();
        }

        public override async Task<SaleDetail> CreateAsync(SaleDetail entity)
        {
            entity.Id = Guid.NewGuid();
            await base.GithubService.CreateFileAsync(base.RepositoryId, base.Path, entity.SaleId, entity.Serialize());

            return entity;
        }

        public override async Task UpdateAsync(SaleDetail entityIn)
        {
            await base.GithubService.UpdateFileAsync(base.RepositoryId, base.Path, entityIn.SaleId, entityIn.Serialize());
        }

        public override Task RemoveAsync(SaleDetail entityIn)
        {
            return base.GithubService.DeleteFileAsync(base.RepositoryId, base.Path, entityIn.SaleId);
        }
    }
}
