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

            //khai bao primary key cho bang GTransaction
            modelBuilder.Entity<GTransaction>()
                .HasKey(t => t.transactionID);
            //khai bao primary key cho bang PlayerQuest
            modelBuilder.Entity<PlayerQuest>()
                .HasKey(pq => new { pq.playerID, pq.questID });
        }
    }
}
