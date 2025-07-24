using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UnivManager.Models;

namespace UnivManager.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bachelier> Bacheliers { get; set; }

    public virtual DbSet<Centre> Centres { get; set; }

    public virtual DbSet<Etablissement> Etablissements { get; set; }

    public virtual DbSet<Matiere> Matieres { get; set; }

    public virtual DbSet<Mention> Mentions { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Personne> Personnes { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Charge la configuration depuis appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("sexe_type", new[] { "F", "M" });

        modelBuilder.Entity<Bachelier>(entity =>
        {
            entity.HasKey(e => e.IdBachelier).HasName("pk_bacheliers");

            entity.ToTable("bacheliers");

            entity.Property(e => e.IdBachelier).HasColumnName("id_bachelier");
            entity.Property(e => e.Annee).HasColumnName("annee");
            entity.Property(e => e.IdCentre).HasColumnName("id_centre");
            entity.Property(e => e.IdEtablissement).HasColumnName("id_etablissement");
            entity.Property(e => e.IdMention).HasColumnName("id_mention");
            entity.Property(e => e.IdOption).HasColumnName("id_option");
            entity.Property(e => e.IdPersonne).HasColumnName("id_personne");
            entity.Property(e => e.Moyenne).HasColumnName("moyenne");
            entity.Property(e => e.NumeroCandidat).HasColumnName("numero_candidat");

            entity.HasOne(d => d.IdCentreNavigation).WithMany(p => p.Bacheliers)
                .HasForeignKey(d => d.IdCentre)
                .HasConstraintName("bacheliers_centre_fkey");

            entity.HasOne(d => d.IdEtablissementNavigation).WithMany(p => p.Bacheliers)
                .HasForeignKey(d => d.IdEtablissement)
                .HasConstraintName("bacheliers_etablissement_fkey");

            entity.HasOne(d => d.IdMentionNavigation).WithMany(p => p.Bacheliers)
                .HasForeignKey(d => d.IdMention)
                .HasConstraintName("bacheliers_mention_fkey");

            entity.HasOne(d => d.IdOptionNavigation).WithMany(p => p.Bacheliers)
                .HasForeignKey(d => d.IdOption)
                .HasConstraintName("bacheliers_serie_fkey");

            entity.HasOne(d => d.IdPersonneNavigation).WithMany(p => p.Bacheliers)
                .HasForeignKey(d => d.IdPersonne)
                .HasConstraintName("bacheliers_id_personne_fkey");
        });

        modelBuilder.Entity<Centre>(entity =>
        {
            entity.HasKey(e => e.IdCentre).HasName("centres_pkey");

            entity.ToTable("centres");

            entity.Property(e => e.IdCentre)
                .HasDefaultValueSql("nextval('centres_id_centre_seq1'::regclass)")
                .HasColumnName("id_centre");
            entity.Property(e => e.Centre1).HasColumnName("centre");
            entity.Property(e => e.IdProvince).HasColumnName("id_province");

            entity.HasOne(d => d.IdProvinceNavigation).WithMany(p => p.Centres)
                .HasForeignKey(d => d.IdProvince)
                .HasConstraintName("fk_centres_provinces");
        });

        modelBuilder.Entity<Etablissement>(entity =>
        {
            entity.HasKey(e => e.IdEtablissement).HasName("etablissements_pkey");

            entity.ToTable("etablissements");

            entity.Property(e => e.IdEtablissement)
                .HasDefaultValueSql("nextval('etablissements_id_etablissement_seq1'::regclass)")
                .HasColumnName("id_etablissement");
            entity.Property(e => e.Etablissement1).HasColumnName("etablissement");
        });

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(e => e.IdMatiere).HasName("matieres_pkey");

            entity.ToTable("matieres");

            entity.Property(e => e.IdMatiere)
                .HasDefaultValueSql("nextval('matieres_id_matiere_seq1'::regclass)")
                .HasColumnName("id_matiere");
            entity.Property(e => e.Matiere1).HasColumnName("matiere");
        });

        modelBuilder.Entity<Mention>(entity =>
        {
            entity.HasKey(e => e.IdMention).HasName("mentions_pkey");

            entity.ToTable("mentions");

            entity.Property(e => e.IdMention)
                .HasDefaultValueSql("nextval('mentions_id_mention_seq1'::regclass)")
                .HasColumnName("id_mention");
            entity.Property(e => e.Max)
                .HasDefaultValue(20)
                .HasColumnName("max");
            entity.Property(e => e.Mention1).HasColumnName("mention");
            entity.Property(e => e.Min)
                .HasDefaultValue(0)
                .HasColumnName("min");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.IdNote).HasName("notes_pkey");

            entity.ToTable("notes");

            entity.Property(e => e.IdNote)
                .HasDefaultValueSql("nextval('notes_id_note_seq1'::regclass)")
                .HasColumnName("id_note");
            entity.Property(e => e.EstOptionnel)
                .HasDefaultValue(false)
                .HasColumnName("est_optionnel");
            entity.Property(e => e.IdBachelier).HasColumnName("id_bachelier");
            entity.Property(e => e.IdMatiere).HasColumnName("id_matiere");
            entity.Property(e => e.Note1).HasColumnName("note");

            entity.HasOne(d => d.IdBachelierNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.IdBachelier)
                .HasConstraintName("notes_id_bachelier_fkey");

            entity.HasOne(d => d.IdMatiereNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.IdMatiere)
                .HasConstraintName("notes_id_matiere_fkey");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.IdOption).HasName("options_pkey");

            entity.ToTable("options");

            entity.Property(e => e.IdOption).HasColumnName("id_option");
            entity.Property(e => e.Serie).HasColumnName("serie");
        });

        modelBuilder.Entity<Personne>(entity =>
        {
            entity.HasKey(e => e.IdPersonne).HasName("personnes_pkey");

            entity.ToTable("personnes");

            entity.Property(e => e.IdPersonne)
                .HasDefaultValueSql("nextval('personnes_id_personne_seq1'::regclass)")
                .HasColumnName("id_personne");
            entity.Property(e => e.DateNaissance).HasColumnName("date_naissance");
            entity.Property(e => e.LieuNaissance).HasColumnName("lieu_naissance");
            entity.Property(e => e.NomPrenom).HasColumnName("nom_prenom");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.IdProvince).HasName("provinces_pkey");

            entity.ToTable("provinces");

            entity.Property(e => e.IdProvince)
                .HasDefaultValueSql("nextval('provinces_id_province_seq1'::regclass)")
                .HasColumnName("id_province");
            entity.Property(e => e.Province1).HasColumnName("province");
        });
        modelBuilder.HasSequence<int>("centres_id_centre_seq");
        modelBuilder.HasSequence<int>("etablissements_id_etablissement_seq");
        modelBuilder.HasSequence<int>("matieres_id_matiere_seq");
        modelBuilder.HasSequence<int>("mentions_id_mention_seq");
        modelBuilder.HasSequence<int>("notes_id_note_seq");
        modelBuilder.HasSequence<int>("options_id_optioin_seq");
        modelBuilder.HasSequence<int>("personnes_id_personne_seq");
        modelBuilder.HasSequence<int>("provinces_id_province_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
