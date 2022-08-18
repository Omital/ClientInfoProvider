using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Radin.EntityFramework;

namespace Radin.Migrator
{
    [DependsOn(typeof(RadinDataModule))]
    public class RadinMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<RadinDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}