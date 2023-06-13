using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using HotSpotWeb.GithubProfiles;

namespace HotSpotWeb.Applications;

public interface IApplicationManager : IDomainService
{
    Task<Application> GetAsync(int id);
    Task<Application> CreateAsync(Application application);
    Task<Application> UpdateAsync(Application application);
    Task DeleteAsync(int id);
    Task<List<Application>> GetAllAsync();
    Task<bool> StartApplication(int id);
    Task CreateGithubRepository(int applicationId, int githubProfileId);
}