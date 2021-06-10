using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using PASR.Configuration.Dto;

namespace PASR.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : PASRAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
