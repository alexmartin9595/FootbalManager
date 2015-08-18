using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Entities;

namespace manager.Data
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("EFDbContext")
        {
            
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<TeamData> TeamDatas { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<Strike> Strikes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<ManagerUser> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                     .HasRequired(m => m.ReceiverUser)
                     .WithMany(t => t.OutputMessages)
                     .HasForeignKey(m => m.ReceiverId)
                     .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                     .HasRequired(m => m.SenderUser)
                     .WithMany(t => t.InputMessages)
                     .HasForeignKey(m => m.SenderId)
                     .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                     .HasRequired(m => m.FirstUser)
                     .WithMany(t => t.FirstUserMatches)
                     .HasForeignKey(m => m.FirstTeamId)
                     .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
                     .HasRequired(m => m.SecondUser)
                     .WithMany(t => t.SecondUserMatches)
                     .HasForeignKey(m => m.SecondTeamId)
                     .WillCascadeOnDelete(false);
            

        }

        
    }
}
