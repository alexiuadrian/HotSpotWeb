using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HotSpotWeb.Applications.Dtos;

namespace HotSpotWeb.Applications;

public interface IApplicationAppService : IApplicationService
{
    Task<List<ApplicationDto>> GetListAsync(GetApplicationListInput input);
    Task<ApplicationDetailsDto> GetDetailsAsync(EntityDto<int> input);
    Task CreateAsync(CreateApplicationInput input);
}