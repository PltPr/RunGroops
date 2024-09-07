using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Data
{
    public class ApplicationDBContext :IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options) : base(options)
        {
            
        }
        public DbSet<Race>Races{ get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Club> Clubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id)
                      .HasColumnType("text")       // Zmieniamy typ na text (lub varchar)
                      .ValueGeneratedNever();      // Wyłączamy automatyczne generowanie wartości (identity)
            });

            base.OnModelCreating(modelBuilder);
        }




    }



}

