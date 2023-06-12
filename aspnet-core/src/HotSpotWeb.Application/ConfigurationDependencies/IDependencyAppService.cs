using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotSpotWeb.ConfigurationDependencies.Dtos;
using HotSpotWeb.Dependencies;
using Abp.Application.Services;

namespace HotSpotWeb.ConfigurationDependencies
{
    public interface IDependencyAppService : IApplicationService
    {
        Task<Dependency> GetAsync(int id);
        Task<List<Dependency>> GetListAsync(GetDependenciesListInput input);
        Task CreateAsync(CreateDependencyDto input);
        Task DeleteAsync(int id);
    }
}
