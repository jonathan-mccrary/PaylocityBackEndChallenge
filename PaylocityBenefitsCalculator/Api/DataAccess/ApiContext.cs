using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.DataAccess
{
	public class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BenefitsCalculator");
        }

        

		public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}

