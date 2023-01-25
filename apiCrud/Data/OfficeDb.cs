using apiCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace apiCrud.Data
{
    public class OfficeDb : DbContext
    {

        public OfficeDb(DbContextOptions<OfficeDb> options) : base (options) { }
        public DbSet<Employee> Employees => Set<Employee>();
    }
}
