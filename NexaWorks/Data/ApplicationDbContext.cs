using NexaWorks.Entities;
using Microsoft.EntityFrameworkCore;

namespace NexaWorks.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Products {  get; set; }
        public DbSet<SystemOs> SystemsOs { get; set; }
        public DbSet<VersionProduct> VersionProducts { get; set; }
        public DbSet<VersionOs> VersionsOs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=NexaWorksDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resolution>()
                .HasIndex(t => t.TicketId)
                .IsUnique();

            modelBuilder.Entity<Resolution>()
                .HasOne<Ticket>()
                .WithOne(t => t.Resolution)
                .HasForeignKey<Resolution>(t => t.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.VersionOs)
                .WithMany()
                .HasForeignKey(t => t.VersionOsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.StatusTicket)
                .WithMany()
                .HasForeignKey(t => t.StatusTicketId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VersionProduct>()
                .HasOne(v => v.Product)
                .WithMany()
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VersionOs>()
                .HasOne(v => v.VersionProduct)
                .WithMany()
                .HasForeignKey(v => v.VersionProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VersionOs>()
                .HasOne(v => v.SystemOs)
                .WithMany()
                .HasForeignKey(v => v.SystemOsId)
                .OnDelete(DeleteBehavior.NoAction);


            // Point d'entrée du peuplement de la base (données de test)
            // Pour créer une base sans données de test : supprimer cette ligne
            // ainsi que le fichier Data/SeedData.cs et le dossier Data/SeedDataContent/
            SeedData.Run(modelBuilder);

        }


    }
}
