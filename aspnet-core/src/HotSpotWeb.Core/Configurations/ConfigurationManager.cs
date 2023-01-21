using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace HotSpotWeb.Configurations;

public class ConfigurationManager : IConfigurationManager
{
    private readonly IRepository<Configuration, int> _configurationRepository;
    
    public ConfigurationManager(IRepository<Configuration, int> configurationRepository)
    {
        _configurationRepository = configurationRepository;
    }
    
    public Task<Configuration> GetAsync(int id)
    {
        var @configuration = _configurationRepository.FirstOrDefaultAsync(id);
        
        if (@configuration == null)
        {
            throw new UserFriendlyException("Could not find the configuration, maybe it's deleted.");
        }
        
        return @configuration;
    }

    public async Task<Configuration> CreateAsync(Configuration configuration)
    {
        return await _configurationRepository.InsertAsync(configuration);
    }

    public Task<Configuration> UpdateAsync(Configuration configuration)
    {
        return _configurationRepository.UpdateAsync(configuration);
    }

    public Task DeleteAsync(int id)
    {
        var @configuration = _configurationRepository.DeleteAsync(id);
        
        if (@configuration == null)
        {
            throw new UserFriendlyException("Could not find the configuration, maybe it's already deleted.");
        }
        
        return @configuration;
    }

    public Task<List<Configuration>> GetAllAsync()
    {
        return _configurationRepository.GetAllListAsync();
    }
}