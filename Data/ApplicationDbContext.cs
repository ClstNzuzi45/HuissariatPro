using HuissariatPro.Models;
using Microsoft.EntityFrameworkCore;

namespace HuissariatPro.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Huissier> Huissiers => Set<Huissier>();
    public DbSet<Etude> Etudes => Set<Etude>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Dossier> Dossiers => Set<Dossier>();
    public DbSet<Acte> Actes => Set<Acte>();
    public DbSet<Audience> Audiences => Set<Audience>();
    public DbSet<Paiement> Paiements => Set<Paiement>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // ===== FIX CASCADE MULTIPLE =====
        // Huissier -> Dossiers : pas de cascade
        b.Entity<Dossier>()
            .HasOne(d => d.Huissier)
            .WithMany(h => h.Dossiers)
            .HasForeignKey(d => d.HuissierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Huissier -> Actes : pas de cascade
        b.Entity<Acte>()
            .HasOne(a => a.Huissier)
            .WithMany(h => h.Actes)
            .HasForeignKey(a => a.HuissierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Dossier -> Actes : pas de cascade
        b.Entity<Acte>()
            .HasOne(a => a.Dossier)
            .WithMany(d => d.Actes)
            .HasForeignKey(a => a.DossierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Dossier -> Audiences
        b.Entity<Audience>()
            .HasOne(a => a.Dossier)
            .WithMany(d => d.Audiences)
            .HasForeignKey(a => a.DossierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Huissier -> Audiences
        b.Entity<Audience>()
            .HasOne(a => a.Huissier)
            .WithMany()
            .HasForeignKey(a => a.HuissierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Dossier -> Paiements
        b.Entity<Paiement>()
            .HasOne(p => p.Dossier)
            .WithMany(d => d.Paiements)
            .HasForeignKey(p => p.DossierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Client -> Dossiers
        b.Entity<Dossier>()
            .HasOne(d => d.Client)
            .WithMany(c => c.Dossiers)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Etude -> Huissiers
        b.Entity<Huissier>()
            .HasOne(h => h.Etude)
            .WithMany(e => e.Huissiers)
            .HasForeignKey(h => h.EtudeId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===== Filtres globaux =====
        b.Entity<Huissier>().HasQueryFilter(h => !h.EstSupprime);
        b.Entity<Dossier>().HasQueryFilter(d => !d.EstSupprime);
        b.Entity<Acte>().HasQueryFilter(a => !a.EstSupprime);
        b.Entity<Audience>().HasQueryFilter(a => !a.EstSupprime);
        b.Entity<Paiement>().HasQueryFilter(p => !p.EstSupprime);
        b.Entity<Etude>().HasQueryFilter(e => !e.EstSupprime);
        b.Entity<Client>().HasQueryFilter(c => !c.EstSupprime);

        // ===== Unicité =====
        b.Entity<Huissier>().HasIndex(h => h.Matricule).IsUnique();
        b.Entity<Dossier>().HasIndex(d => d.NumeroDossier).IsUnique();
        b.Entity<Acte>().HasIndex(a => a.NumeroActe).IsUnique();
        b.Entity<Paiement>().HasIndex(p => p.Reference).IsUnique();

        // ===== Ignorés =====
        b.Entity<Dossier>().Ignore(d => d.TotalDu);
        b.Entity<Dossier>().Ignore(d => d.TotalPaye);
        b.Entity<Dossier>().Ignore(d => d.ResteAPayer);
        b.Entity<Huissier>().Ignore(h => h.NomComplet);
        b.Entity<Client>().Ignore(c => c.NomComplet);
    }
}