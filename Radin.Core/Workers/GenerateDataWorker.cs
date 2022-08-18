
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Quartz;
using Abp.Timing;
using Quartz;
using Radin.Test;
using System.Threading.Tasks;

namespace Radin.Workers
{
    public class GenerateDataWorker : JobBase, ITransientDependency
    {
        public readonly IRepository<BaseData> _baseDataRepo;


        public GenerateDataWorker(IRepository<BaseData> baseDataRepo)
        {
            _baseDataRepo = baseDataRepo;
        }



        [UnitOfWork]
        public override async Task Execute(IJobExecutionContext context)
        {

            BaseData bd = new BaseData()
            {
                Name = Clock.Now.Ticks.ToString()
            };
            await _baseDataRepo.InsertAsync(bd);

        }
    }

}
