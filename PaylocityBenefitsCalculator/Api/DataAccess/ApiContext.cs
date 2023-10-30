using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.DataAccess
{
    /// <summary>
    /// This is a simple in-memory database context for storing Employees and Dependents locally. 
    /// </summary>
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

