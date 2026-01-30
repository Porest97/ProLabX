using Microsoft.EntityFrameworkCore;
using ProLab.Areas.HockeyData.Models.DataModels;

namespace ProLab.Areas.HockeyData.Data
{
    public class HockeyDataContext : DbContext
    {
        public HockeyDataContext(DbContextOptions<HockeyDataContext> options)
            : base(options)
        {
        }

        public DbSet<HockeyArena> HockeyArenas => Set<HockeyArena>();
        public DbSet<HockeyPlayer> HockeyPlayers => Set<HockeyPlayer>();
        public DbSet<HockeySeries> HockeySeries => Set<HockeySeries>();
        public DbSet<HockeyTeam> HockeyTeams => Set<HockeyTeam>();
        public DbSet<HockeyGame> HockeyGames => Set<HockeyGame>();
        public DbSet<HockeyGameStatus> HockeyGameStatuses => Set<HockeyGameStatus>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HockeyGame>()
                .HasOne(g => g.HomeTeam)
                .WithMany()
                .HasForeignKey(g => g.HockeyTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HockeyGame>()
                .HasOne(g => g.AwayTeam)
                .WithMany()
                .HasForeignKey(g => g.HockeyTeamId1)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}