using System.Threading.Tasks;
using HotSpotWeb.Configuration.Dto;

namespace HotSpotWeb.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
