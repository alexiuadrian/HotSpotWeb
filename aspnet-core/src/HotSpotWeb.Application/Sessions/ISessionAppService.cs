using System.Threading.Tasks;
using Abp.Application.Services;
using HotSpotWeb.Sessions.Dto;

namespace HotSpotWeb.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
