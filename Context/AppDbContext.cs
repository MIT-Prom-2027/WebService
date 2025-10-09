// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutilAdmin.Models;

namespace UnivManager.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Vos entités métiers
        public virtual DbSet<Administration> Administrations { get; set; }
        public virtual DbSet<Bachelier> Bacheliers { get; set; }
        public virtual DbSet<Centre> Centres { get; set; }
        public virtual DbSet<Etablissement> Etablissements { get; set; }
        public virtual DbSet<Historique> Historiques { get; set; }
        public virtual DbSet<Matiere> Matieres { get; set; }
        public virtual DbSet<Mention> Mentions { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Personne> Personnes { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Indispensable pour Identity

            // Configuration simplifiée - laissez EF gérer la plupart des conventions
            
            // Seulement les configurations personnalisées nécessaires
            modelBuilder.Entity<Bachelier>(entity =>
            {
                entity.HasKey(e => e.IdBachelier);
                entity.Property(e => e.NumeroCandidat).IsRequired();
                entity.Property(e => e.Moyenne).HasColumnType("decimal(18,2)");
                
                // Relations
                entity.HasOne(d => d.IdPersonneNavigation)
                    .WithMany(p => p.Bacheliers)
                    .HasForeignKey(d => d.IdPersonne);
                    
                entity.HasOne(d => d.IdCentreNavigation)
                    .WithMany(p => p.Bacheliers)
                    .HasForeignKey(d => d.IdCentre);
                    
                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.Bacheliers)
                    .HasForeignKey(d => d.IdEtablissement);
                    
                entity.HasOne(d => d.IdMentionNavigation)
                    .WithMany(p => p.Bacheliers)
                    .HasForeignKey(d => d.IdMention);
                    
                entity.HasOne(d => d.IdOptionNavigation)
                    .WithMany(p => p.Bacheliers)
                    .HasForeignKey(d => d.IdOption);
            });

            modelBuilder.Entity<Centre>(entity =>
            {
                entity.HasKey(e => e.IdCentre);
                entity.Property(e => e.NomCentre).IsRequired().HasMaxLength(200);
                
                entity.HasOne(d => d.IdProvinceNavigation)
                    .WithMany(p => p.Centres)
                    .HasForeignKey(d => d.IdProvince);
            });

            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.HasKey(e => e.IdEtablissement);
                entity.Property(e => e.NomEtablissement).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Mention>(entity =>
            {
                entity.HasKey(e => e.IdMention);
                entity.Property(e => e.NomMention).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Min).HasDefaultValue(0);
                entity.Property(e => e.Max).HasDefaultValue(20);
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.IdOption);
                entity.Property(e => e.Serie).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Personne>(entity =>
            {
                entity.HasKey(e => e.IdPersonne);
                entity.Property(e => e.NomPrenom).IsRequired().HasMaxLength(200);
                entity.Property(e => e.LieuNaissance).HasMaxLength(200);
                entity.Property(p => p.Sexe)
                  .HasMaxLength(1)
                  .IsRequired()
                  .HasDefaultValue("M");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.IdProvince);
                entity.Property(e => e.NomProvince).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.IdNote);
                entity.Property(e => e.ValeurNote).HasColumnType("decimal(18,2)");
                entity.Property(e => e.EstOptionnel).HasDefaultValue(false);
                
                entity.HasOne(d => d.IdBachelierNavigation)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.IdBachelier);
                    
                entity.HasOne(d => d.IdMatiereNavigation)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.IdMatiere);
            });

            modelBuilder.Entity<Matiere>(entity =>
            {
                entity.HasKey(e => e.IdMatiere);
                entity.Property(e => e.NomMatiere).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Administration>(entity =>
            {
                entity.HasKey(e => e.IdAdmin);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Historique>(entity =>
            {
                entity.HasKey(e => e.IdHistorique);
                entity.Property(e => e.DateEvenement).HasDefaultValueSql("NOW()");
                
                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Historiques)
                    .HasForeignKey(d => d.AdminId);
                    
                entity.HasOne(d => d.IdProvinceNavigation)
                    .WithMany(p => p.Historiques)
                    .HasForeignKey(d => d.IdProvince);
            });
        }
    }
}