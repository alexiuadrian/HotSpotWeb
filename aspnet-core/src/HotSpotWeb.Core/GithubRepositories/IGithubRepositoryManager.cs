using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace HotSpotWeb.GithubRepositories
{
	public interface IGithubRepositoryManager : IDomainService
    {
        Task<GithubRepository> GetAsync(int id);
        Task<GithubRepository> CreateAsync(GithubRepository githubRepository);
        Task<GithubRepository> UpdateAsync(GithubRepository githubRepository);
        Task DeleteAsync(int id);
        Task<List<GithubRepository>> GetAllAsync();
    }
}

