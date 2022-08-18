using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Radin.EntityFramework.Repositories
{
    public abstract class RadinRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<RadinDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RadinRepositoryBase(IDbContextProvider<RadinDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class RadinRepositoryBase<TEntity> : RadinRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected RadinRepositoryBase(IDbContextProvider<RadinDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
