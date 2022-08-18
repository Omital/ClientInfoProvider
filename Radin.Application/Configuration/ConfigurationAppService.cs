using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Radin.Configuration.Dto;

namespace Radin.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : RadinAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
