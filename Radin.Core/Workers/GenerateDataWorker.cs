
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Quartz;
using Abp.Runtime.Session;
using Abp.Timing;
using Quartz;
using Radin.Test;
using System.Threading.Tasks;

namespace Radin.Workers
{
    public class GenerateDataWorker : JobBase, ITransientDependency
    {
        public readonly IRepository<BaseData> _baseDataRepo;
        public IAbpSession AbpSession { get; set; }

        public GenerateDataWorker(IRepository<BaseData> baseDataRepo)
        {
            _baseDataRepo = baseDataRepo;
        }



        [UnitOfWork]
        public override async Task Execute(IJobExecutionContext context)
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
