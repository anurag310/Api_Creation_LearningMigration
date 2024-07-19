using Api_Creation_LearningMigration.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Api_Creation_LearningMigration.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Login> LoginTbl { get; set; }
        public DbSet<Register> RegisterTbl { get; set; }
        public DbSet<Employee> EmployeeTbl { get; set; }
        public DbSet<Department> DepartmentsTbl { get; set; }
    }
}
