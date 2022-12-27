using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace HotSpotWeb.Applications;

public interface IApplicationManager : IDomainService
{
    Task<Application> GetAsync(int id);
    Task<Application> CreateAsync(Application application);
    Task<Application> UpdateAsync(Application application);
    Task DeleteAsync(int id);
    Task<List<Application>> GetAllAsync();
    Task<Boolean> StartApplication(int id);
}