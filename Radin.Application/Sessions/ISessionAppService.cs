using System.Threading.Tasks;
using Abp.Application.Services;
using Radin.Sessions.Dto;

namespace Radin.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
