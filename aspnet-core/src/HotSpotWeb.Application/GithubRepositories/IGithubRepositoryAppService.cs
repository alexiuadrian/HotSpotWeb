using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using HotSpotWeb.GithubProfiles;
using HotSpotWeb.GithubProfiles.Dtos;
using HotSpotWeb.GithubRepositories.Dtos;

namespace HotSpotWeb.GithubRepositories;

public interface IGithubRepositoryAppService : IApplicationService
{   
    Task<GithubRepository> GetAsync(int id);
    Task<List<GithubRepository>> GetListAsync(GetGithubRepositoriesListInput input);
    Task CreateAsync(CreateGithubRepositoryDto input);
    Task DeleteAsync(int id);
    Task UpdateAsync(GithubRepository githubRepository);
    Task<int> IsApplicationOnGithub(int applicationId);
}