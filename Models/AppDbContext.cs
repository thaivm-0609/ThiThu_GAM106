using Microsoft.EntityFrameworkCore;

namespace ThiThu.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }

        //khai bao bang trong DB
        public DbSet<Player> Player { get; set; }
        public DbSet<Quest> Quest { get; set; }
        public DbSet<PlayerQuest> PlayerQuest { get; set; }
    }
}
