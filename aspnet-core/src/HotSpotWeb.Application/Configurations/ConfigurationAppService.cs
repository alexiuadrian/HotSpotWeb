using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using HotSpotWeb.Configurations.Dtos;

namespace HotSpotWeb.Configurations;

[AbpAuthorize]
public class ConfigurationAppService : HotSpotWebAppServiceBase, IConfigurationAppService
{
    private readonly IRepository<Configuration, int> _configurationRepository;
    private readonly IConfigurationManager _configurationManager;

    public ConfigurationAppService(IRepository<Configuration, int> configurationRepository, IConfigurationManager configurationManager)
    {
        _configurationRepository = configurationRepository;
        _configurationManager = configurationManager;
    }

    public Task<List<ConfigurationDto>> GetListAsync(GetConfigurationsListInput input)
    {
        var configurations = _configurationRepository
            .GetAll()
            .WhereIf(input.Filter != null, a => a.Name.Contains(input.Filter))
            .ToList();

        if (input.Sorting == "ASC")
        {
            configurations = configurations.OrderBy(a => a.Name).ToList();
        }
        else if (input.Sorting == "DESC")
        {
            configurations = configurations.OrderByDescending(a => a.Name).ToList();
        }

        if (input.MaxResultCount > 100)
        {
            throw new UserFriendlyException("Max result count cannot be greater than 100");
        }

        if (input.MaxResultCount != 0)
        {
            configurations = configurations.Take(input.MaxResultCount).ToList();
        }

        return Task.FromResult(configurations.MapTo<List<ConfigurationDto>>());
    }

    public Task<ConfigurationDto> GetDetailsAsync(EntityDto<int> input)
    {
        var configuration = _configurationRepository.Get(input.Id);

        if (configuration == null)
        {
            throw new UserFriendlyException("Configuration not found");
        }

        return Task.FromResult(configuration.MapTo<ConfigurationDto>());
    }

    public Task CreateAsync(CreateConfigurationDto input)
    {
        Configuration configuration = Configuration.Create(input.Name, input.Language, input.Framework, input.Version,
            input.Description, AbpSession.GetUserId(), input.Dependencies, input.Application);
        return _configurationManager.CreateAsync(configuration);
    }
}