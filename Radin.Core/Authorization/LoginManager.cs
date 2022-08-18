using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using Radin.Authorization.Roles;
using Radin.Authorization.Users;
using Radin.MultiTenancy;
using System.Threading.Tasks;
using System.Transactions;

namespace Radin.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        public new IClientInfoProvider ClientInfoProvider { get; set; }

        public LogInManager(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            RoleManager roleManager)
            : base(
                  userManager,
                  multiTenancyConfig,
                  tenantRepository,
                  unitOfWorkManager,
                  settingManager,
                  userLoginAttemptRepository,
                  userManagementConfig,
                  iocResolver,
                  roleManager)
        {
        }

        protected override async Task SaveLoginAttempt(AbpLoginResult<Tenant, User> loginResult, string tenancyName, string userNameOrEmailAddress)
        {
            using (IUnitOfWorkCompleteHandle uow = UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                int? tenantId = ((loginResult.Tenant != null) ? new int?(loginResult.Tenant.Id) : null);
                using (UnitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    UserLoginAttempt entity = new UserLoginAttempt
                    {
                        TenantId = tenantId,
                        TenancyName = tenancyName,
                        UserId = ((loginResult.User != null) ? new long?(loginResult.User.Id) : null),
                        UserNameOrEmailAddress = userNameOrEmailAddress,
                        Result = loginResult.Result,
                        BrowserInfo = ClientInfoProvider.BrowserInfo,
                        ClientIpAddress = ClientInfoProvider.ClientIpAddress,
                        ClientName = ClientInfoProvider.ComputerName
                    };
                    await UserLoginAttemptRepository.InsertAsync(entity).ConfigureAwait(continueOnCapturedContext: false);
                    await UnitOfWorkManager.Current.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
                    await uow.CompleteAsync().ConfigureAwait(continueOnCapturedContext: false);
                }
            }
            //return base.SaveLoginAttempt(loginResult, tenancyName, userNameOrEmailAddress);
        }
    }
}
