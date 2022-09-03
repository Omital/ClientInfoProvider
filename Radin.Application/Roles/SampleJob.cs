using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.Timing;
using Radin.Test;
using System.Threading.Tasks;

namespace Radin.Roles
{
    public class SampleJob : AsyncBackgroundJob<SampleJobArgs>, ITransientDependency
    {
        public readonly IRepository<BaseData> _baseDataRepo;

        public IAbpSession AbpSession { get; set; }

        public SampleJob(IRepository<BaseData> baseDataRepo)
        {
            _baseDataRepo = baseDataRepo;
        }

        [UnitOfWork(true)]
        public override async Task ExecuteAsync(SampleJobArgs args)
        {
            using (AbpSession.Use(1, 2))
            {
                BaseData bd = new BaseData()
                {
                    Name = Clock.Now.Ticks.ToString()
                };
                await _baseDataRepo.InsertAsync(bd);
            }
        }
    }

}

