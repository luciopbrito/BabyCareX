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
        public BabyCareXContext(DbContextOptions<BabyCareXContext> context) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baba>()
                .HasMany(b => b.BabaCourses)
                .WithOne(bc => bc.Baba)
                .HasForeignKey(bc => bc.BabaId)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<Baba>()
                .HasMany(b => b.BabaProvideServices)
                .WithOne(bp => bp.Baba)
                .HasForeignKey(bp => bp.BabaId)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<Baba>()
                .HasMany(b => b.BabaCapacities)
                .WithOne(bc => bc.Baba)
                .HasForeignKey(bc => bc.BabaId)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<Baba>()
                .HasMany(b => b.Schedules)
                .WithOne(bc => bc.Baba)
                .HasForeignKey(bc => bc.BabaId)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<Family>()
                .HasMany(b => b.Schedules)
                .WithOne(bc => bc.Family)
                .HasForeignKey(bc => bc.FamilyId)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<Family>()
                .HasMany(b => b.Children)
                .WithOne(bc => bc.Family)
                .HasForeignKey(bc => bc.FamilyId)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<KindNanny>()
                .HasMany(k => k.BabaProvideServices)
                .WithOne(b => b.KindNanny)
                .HasForeignKey(b => b.KindNannyId)
                .HasPrincipalKey(k => k.Id);


            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Description = "ATIVO", CreatedAt = DateTime.Now },
                new Status { Id = 2, Description = "INATIVO", CreatedAt = DateTime.Now },
                new Status { Id = 3, Description = "CANCELADO", CreatedAt = DateTime.Now },
                new Status { Id = 4, Description = "DELETADO", CreatedAt = DateTime.Now }
            );
           
            modelBuilder.Entity<KindNanny>().HasData(
                new KindNanny { Id= 1, Name = "Baba", Description = "Baba para qualquer momento", CreatedAt = DateTime.Now, StatusId = Domain.Enums.EStatus.ATIVO },
                new KindNanny { Id= 2, Name = "Babysitter", Description = "Baba para dias específicos", CreatedAt = DateTime.Now, StatusId = Domain.Enums.EStatus.ATIVO },
                new KindNanny { Id= 3, Name = "Au Pair", Description = "Baba para fora do pais", CreatedAt = DateTime.Now, StatusId = Domain.Enums.EStatus.ATIVO }
            );
        }
    }
}