using System.Threading.Tasks;
using Abp.Application.Services;
using Radin.Authorization.Accounts.Dto;

namespace Radin.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
