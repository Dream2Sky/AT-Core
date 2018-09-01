using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace AT_Core.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}