using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Models;

namespace MVCWebAPP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Mouse> Mice {get; set; }
           
    }
}
