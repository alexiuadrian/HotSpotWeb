using Abp.Application.Services;
using HotSpotWeb.MultiTenancy.Dto;

namespace HotSpotWeb.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

