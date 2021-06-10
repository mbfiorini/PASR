using System.Threading.Tasks;
using PASR.Models.TokenAuth;
using PASR.Web.Controllers;
using Shouldly;
using Xunit;

namespace PASR.Web.Tests.Controllers
{
    public class HomeController_Tests: PASRWebTestBase
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