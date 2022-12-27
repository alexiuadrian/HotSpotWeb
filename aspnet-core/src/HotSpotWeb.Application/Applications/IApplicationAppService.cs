using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HotSpotWeb.Applications.Dtos;

namespace HotSpotWeb.Applications;

public interface IApplicationAppService : IApplicationService
{
    Task<ListResultDto<ApplicationListDto>> GetListAsync(GetApplicationListInput input);
}