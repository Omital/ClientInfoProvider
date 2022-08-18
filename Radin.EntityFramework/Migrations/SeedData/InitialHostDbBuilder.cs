using Radin.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Radin.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly RadinDbContext _context;

        public InitialHostDbBuilder(RadinDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
