using Abp.Modules;
using Abp.Quartz;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.Zero.Configuration;
using Quartz;
using Radin.Api;
using Radin.Workers;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Radin.Web
{
    [DependsOn(
        typeof(RadinDataModule),
        typeof(RadinApplicationModule),
        typeof(RadinWebApiModule),
        typeof(AbpWebSignalRModule),
            typeof(AbpQuartzModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class RadinWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<RadinNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {

            IocManager.Resolve<IQuartzScheduleJobManager>().ScheduleAsync<GenerateDataWorker>(job =>
            {
                job.WithIdentity("GenerateDataWorker", "GenerateDataWorker")
                 .WithDescription("GenerateDataWorker");
            }, trigger =>
            {
                trigger.StartNow().WithSchedule(CronScheduleBuilder.CronSchedule("0 0/2 * * * ?")).Build();
            });


            base.PostInitialize();
        }
    }
}
