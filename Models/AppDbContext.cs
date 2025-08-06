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

        public DbSet<Item> Item { get; set; }

        public DbSet<GTransaction> GTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GTransaction>()
                .HasKey(e => e.transactionID);

            modelBuilder.Entity<PlayerQuest>()
                .HasKey(q => new { q.playerID, q.questID });
        }
    }
}
