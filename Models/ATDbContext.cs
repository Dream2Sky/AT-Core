using AT_Core.Models.Entity;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AT_Core.Models
{
    public class ATDbContext : DbContext
    {
        public ATDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}