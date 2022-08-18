using Abp;
using Abp.Domain.Entities.Auditing;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Radin.Authorization;
using Radin.Authorization.Roles;
using Radin.Authorization.Users;
using Radin.Configuration;
using Radin.MultiTenancy;
using System.Reflection;

namespace Radin
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class RadinCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            Configuration.EntityHistory.IsEnabled = true;

            Configuration.EntityHistory.Selectors.Add(
       new NamedTypeSelector(
           "Abp.FullAuditedEntities",
           type => typeof(IFullAudited).IsAssignableFrom(type)
       )
   );


            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = RadinConsts.MultiTenancyEnabled;

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    RadinConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Radin.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<RadinAuthorizationProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();

            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = RadinConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
