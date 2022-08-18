using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Radin.MultiTenancy.Dto;

namespace Radin.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
