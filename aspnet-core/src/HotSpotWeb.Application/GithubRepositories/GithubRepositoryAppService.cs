using System.Collections.Generic;
using System.Threading.Tasks;
using HotSpotWeb.GithubProfiles;
using HotSpotWeb.GithubRepositories.Dtos;

namespace HotSpotWeb.GithubRepositories;

public class GithubRepositoryAppService : HotSpotWebAppServiceBase, IGithubRepositoryAppService
{
    private readonly IGithubRepositoryManager _githubRepositoryManager;
    private readonly IGithubProfileManager _githubProfileManager;
    public GithubRepositoryAppService(IGithubRepositoryManager githubRepositoryManager, IGithubProfileManager githubProfileManager)
    {
        _githubRepositoryManager = githubRepositoryManager;
        _githubProfileManager = githubProfileManager;
    }

    public Task<GithubRepository> GetAsync(int id)
    {
        return _githubRepositoryManager.GetAsync(id);
    }

    public Task<List<GithubRepository>> GetListAsync(GetGithubRepositoriesListInput input)
    {
        return _githubRepositoryManager.GetAllAsync();
    }

    public async Task CreateAsync(CreateGithubRepositoryDto input)
    {
        var githubProfile = await _githubProfileManager.GetAsync(input.GithubProfileId);
        var githubRepository = GithubRepository.Create(input.RepositoryName, input.Description, githubProfile);
        await _githubRepositoryManager.CreateAsync(githubRepository);
    }

    public Task DeleteAsync(int id)
    {
        return _githubRepositoryManager.DeleteAsync(id);
    }
}