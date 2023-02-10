using AngularCalisma.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCalisma.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options):base(options) 
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
