using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Radin.Roles.Dto;
using Radin.Users.Dto;

namespace Radin.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}