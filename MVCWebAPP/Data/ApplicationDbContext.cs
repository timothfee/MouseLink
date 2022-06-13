using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Models;

namespace MVCWebAPP.Data
{
    public class ApplicationDbContext : IdentityDbContext<MouseUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Mouse> Mice {get; set; }
           
    }
}
