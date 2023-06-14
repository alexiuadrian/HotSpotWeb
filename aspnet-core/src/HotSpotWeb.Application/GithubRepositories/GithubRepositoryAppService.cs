using System.Collections.Generic;
using System.Threading.Tasks;
using HotSpotWeb.Applications;
using HotSpotWeb.GithubProfiles;
using HotSpotWeb.GithubRepositories.Dtos;

namespace HotSpotWeb.GithubRepositories;

public class GithubRepositoryAppService : HotSpotWebAppServiceBase, IGithubRepositoryAppService
{
    private readonly IGithubRepositoryManager _githubRepositoryManager;
    private readonly IGithubProfileManager _githubProfileManager;
    private readonly IApplicationManager _applicationManager;
    public GithubRepositoryAppService(IGithubRepositoryManager githubRepositoryManager, IGithubProfileManager githubProfileManager,
        IApplicationManager applicationManager)
    {
        _githubRepositoryManager = githubRepositoryManager;
        _githubProfileManager = githubProfileManager;
        _applicationManager = applicationManager;
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
        var application = await _applicationManager.GetAsync(input.ApplicationId);
        var githubProfile = await _githubProfileManager.GetAsync(input.GithubProfileId);
        var githubRepository = GithubRepository.Create(input.RepositoryName, input.Description, githubProfile, application, null);
        var url = await CommandsServiceHelper.SendCreateGithubRepository(githubRepository);
        githubRepository.Url = url;
        await _githubRepositoryManager.CreateAsync(githubRepository);
    }

    public Task DeleteAsync(int id)
    {
        return _githubRepositoryManager.DeleteAsync(id);
    }

    public async Task<int> IsApplicationOnGithub(int applicationId)
    {
        return await _githubRepositoryManager.IsApplicationOnGithub(applicationId);
    }

    public async Task UpdateAsync(GithubRepository githubRepository)
    {
        await _githubRepositoryManager.UpdateAsync(githubRepository);
    }
    
    public async Task GenerateAndUploadGithubRepository(int id)
    {
        var githubRepository = await _githubRepositoryManager.GetAsync(id);
        await CommandsServiceHelper.SendGenerateAndUploadToGithub(githubRepository);
    }

    public async Task<GithubRepository> findGithubRepositoryForApplicationId(int applicationId)
    {
        var application = await _applicationManager.GetAsync(applicationId);

        return await _githubRepositoryManager.findGithubRepositoryForApplicationId(application);
    }
}