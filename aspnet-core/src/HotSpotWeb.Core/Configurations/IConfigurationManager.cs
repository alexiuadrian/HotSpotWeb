using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace HotSpotWeb.Configurations;

public interface IConfigurationManager : IDomainService
{
    Task<Configuration> GetAsync(int id);
    Task<Configuration> CreateAsync(Configuration configuration);
    Task<Configuration> UpdateAsync(Configuration configuration);
    Task DeleteAsync(int id);
    Task<List<Configuration>> GetAllAsync();
}