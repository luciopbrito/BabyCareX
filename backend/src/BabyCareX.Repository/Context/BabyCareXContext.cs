using BabyCareX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository.Context
{
    public class BabyCareXContext : DbContext
    {
        public DbSet<Status> Status { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Baba> Babas { get; set; }
        public DbSet<BabaCourse> BabaCourses { get; set; }
        public DbSet<BabaCapacity> BabaCapacities { get; set; }
        public DbSet<BabaProvideService> BabaProvideServices { get; set; }
        public DbSet<KindNanny> KindNannies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasKey(s => new { s.FamilyId, s.BabasId });
        }
    }
}