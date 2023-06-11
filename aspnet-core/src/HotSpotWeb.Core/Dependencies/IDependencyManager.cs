using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.Dependencies
{
    public interface IDependencyManager : IDomainService
    {
        Task<Dependency> GetAsync(int id);
        Task<List<Dependency>> GetAllAsync();
        Task CreateAsync(Dependency dependency);
        Task DeleteAsync(int id);
        Task<Dependency> UpdateAsync(Dependency dependency);
    }
}
