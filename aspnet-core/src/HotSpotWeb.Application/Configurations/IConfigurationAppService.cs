using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HotSpotWeb.Configurations.Dtos;

namespace HotSpotWeb.Configurations;

public interface IConfigurationAppService : IApplicationService
{
    Task<List<ConfigurationDto>> GetListAsync(GetConfigurationsListInput input);
    Task<ConfigurationDto> GetDetailsAsync(EntityDto<int> input);
    Task CreateAsync(CreateConfigurationDto input);
    Task DeleteAsync(int id);
}