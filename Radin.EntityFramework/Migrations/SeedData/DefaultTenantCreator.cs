using System.Linq;
using Radin.EntityFramework;
using Radin.MultiTenancy;

namespace Radin.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly RadinDbContext _context;

        public DefaultTenantCreator(RadinDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
