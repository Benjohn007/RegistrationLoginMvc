using Microsoft.EntityFrameworkCore;

namespace LoginAndRegistration.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<UserAccount> userAccounts { get; set; }
    }
}
