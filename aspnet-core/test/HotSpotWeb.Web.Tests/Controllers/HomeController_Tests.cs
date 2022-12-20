using System.Threading.Tasks;
using HotSpotWeb.Models.TokenAuth;
using HotSpotWeb.Web.Controllers;
using Shouldly;
using Xunit;

namespace HotSpotWeb.Web.Tests.Controllers
{
    public class HomeController_Tests: HotSpotWebWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}