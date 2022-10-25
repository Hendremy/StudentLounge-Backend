using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentLounge_Backend.Models
{
    public class StudentLoungeDbContext : IdentityDbContext<StudentLoungeUser>
    {
        public StudentLoungeDbContext(DbContextOptions<StudentLoungeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<StudentLoungeUser> Users { get; set; }
        //public DbSet<OAuthAccount> OAuthAccounts { get; set; }

    }
}
