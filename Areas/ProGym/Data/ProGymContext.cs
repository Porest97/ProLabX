using Microsoft.EntityFrameworkCore;
using ProLab.Areas.ProGym.Models.DataModels;

namespace ProLab.Areas.ProGym.Data
{
    public class ProGymContext : DbContext
    {
        public ProGymContext(DbContextOptions<ProGymContext> options)
            : base(options)
        {
        }

        // ===== CORE ENTITY =====
        public DbSet<ProGymWorkOut> ProGymWorkOuts => Set<ProGymWorkOut>();

        // ===== EXERCISE CATALOG =====
        public DbSet<ProGymExercise> ProGymExercises => Set<ProGymExercise>();

        // ===== WORKOUT LOG =====
        public DbSet<ProGymWorkoutExercise> ProGymWorkoutExercises => Set<ProGymWorkoutExercise>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== DATETIME RULES (PG-SAFE) =====
            modelBuilder.Entity<ProGymWorkOut>(entity =>
            {
                entity.Property(e => e.StartDate)
                      .HasColumnType("timestamp with time zone");

                entity.Property(e => e.EndDate)
                      .HasColumnType("timestamp with time zone");
            });

            // ===== RELATION: WORKOUT ↔ EXERCISES =====
            modelBuilder.Entity<ProGymWorkoutExercise>(entity =>
            {
                entity.HasOne(x => x.ProGymWorkOut)
                      .WithMany()
                      .HasForeignKey(x => x.ProGymWorkOutId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.ProGymExercise)
                      .WithMany()
                      .HasForeignKey(x => x.ProGymExerciseId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}