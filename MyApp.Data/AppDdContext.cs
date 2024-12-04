using MyApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Data
{
    public class AppDdContext:DbContext
    {
        public AppDdContext(DbContextOptions<AppDdContext> options): base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
    }
}
