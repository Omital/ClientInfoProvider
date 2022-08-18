using System.Threading.Tasks;
using Abp.Application.Services;
using Radin.Configuration.Dto;

namespace Radin.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}