using System.Threading.Tasks;
using Abp.Application.Services;
using HotSpotWeb.Authorization.Accounts.Dto;

namespace HotSpotWeb.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
