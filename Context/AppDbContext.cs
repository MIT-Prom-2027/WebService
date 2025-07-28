using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UnivManager.Models;

namespace UnivManager.Context;

// Classe de contexte de base de données pour Entity Framework Core
public partial class AppDbContext : DbContext
{
    // Constructeur prenant les options de configuration du contexte
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Définition des tables (DbSet) correspondant aux entités du modèle
    public virtual DbSet<administration> administrations { get; set; }
    public virtual DbSet<bachelier> bacheliers { get; set; }
    public virtual DbSet<centre> centres { get; set; }
    public virtual DbSet<etablissement> etablissements { get; set; }
    public virtual DbSet<historique> historiques { get; set; }
    public virtual DbSet<matiere> matieres { get; set; }
    public virtual DbSet<mention> mentions { get; set; }
    public virtual DbSet<note> notes { get; set; }
    public virtual DbSet<option> options { get; set; }
    public virtual DbSet<personne> personnes { get; set; }
    public virtual DbSet<province> provinces { get; set; }

    // Méthode de configuration du modèle (appelée lors de la création du modèle)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Déclaration d'un type énuméré PostgreSQL pour le sexe
        modelBuilder.HasPostgresEnum("sexe_type", new[] { "F", "M" });

        // Configuration de l'entité administration
        modelBuilder.Entity<administration>(entity =>
        {
            entity.HasKey(e => e.id_admin).HasName("administration_pkey");
            entity.ToTable("administration");
            entity.HasIndex(e => e.username, "administration_username_key").IsUnique(); // Username unique
        });

        // Configuration de l'entité bachelier et de ses relations
        modelBuilder.Entity<bachelier>(entity =>
        {
            entity.HasKey(e => e.id_bachelier).HasName("pk_bacheliers");
            entity.Property(e => e.id_bachelier).HasDefaultValueSql("nextval('bacheliers_id_bachelier_seq'::regclass)");
            // Relations avec centre, établissement, mention, option, personne
            entity.HasOne(d => d.id_centreNavigation).WithMany(p => p.bacheliers)
                .HasForeignKey(d => d.id_centre)
                .HasConstraintName("bacheliers_centre_fkey");
            entity.HasOne(d => d.id_etablissementNavigation).WithMany(p => p.bacheliers)
                .HasForeignKey(d => d.id_etablissement)
                .HasConstraintName("bacheliers_etablissement_fkey");
            entity.HasOne(d => d.id_mentionNavigation).WithMany(p => p.bacheliers)
                .HasForeignKey(d => d.id_mention)
                .HasConstraintName("bacheliers_mention_fkey");
            entity.HasOne(d => d.id_optionNavigation).WithMany(p => p.bacheliers)
                .HasForeignKey(d => d.id_option)
                .HasConstraintName("bacheliers_serie_fkey");
            entity.HasOne(d => d.id_personneNavigation).WithMany(p => p.bacheliers)
                .HasForeignKey(d => d.id_personne)
                .HasConstraintName("bacheliers_id_personne_fkey");
        });

        // Configuration de l'entité centre et de sa relation avec province
        modelBuilder.Entity<centre>(entity =>
        {
            entity.HasKey(e => e.id_centre).HasName("centres_pkey");
            entity.Property(e => e.id_centre).HasDefaultValueSql("nextval('centres_id_centre_seq'::regclass)");
            entity.HasOne(d => d.id_provinceNavigation).WithMany(p => p.centres)
                .HasForeignKey(d => d.id_province)
                .HasConstraintName("fk_centres_provinces");
        });

        // Configuration de l'entité etablissement
        modelBuilder.Entity<etablissement>(entity =>
        {
            entity.HasKey(e => e.id_etablissement).HasName("etablissements_pkey");
            entity.Property(e => e.id_etablissement).HasDefaultValueSql("nextval('etablissements_id_etablissement_seq'::regclass)");
        });

        // Configuration de l'entité historique et de ses relations
        modelBuilder.Entity<historique>(entity =>
        {
            entity.HasKey(e => e.id_historique).HasName("historique_pkey");
            entity.ToTable("historique");
            entity.Property(e => e.date_evenement)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.id_province).ValueGeneratedOnAdd();
            entity.HasOne(d => d.admin).WithMany(p => p.historiques)
                .HasForeignKey(d => d.admin_id)
                .HasConstraintName("historique_admin_id_fkey");
            entity.HasOne(d => d.id_provinceNavigation).WithMany(p => p.historiques)
                .HasForeignKey(d => d.id_province)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_historique_provinces");
        });

        // Configuration de l'entité matiere
        modelBuilder.Entity<matiere>(entity =>
        {
            entity.HasKey(e => e.id_matiere).HasName("matieres_pkey");
            entity.Property(e => e.id_matiere).HasDefaultValueSql("nextval('matieres_id_matiere_seq'::regclass)");
        });

        // Configuration de l'entité mention
        modelBuilder.Entity<mention>(entity =>
        {
            entity.HasKey(e => e.id_mention).HasName("mentions_pkey");
            entity.Property(e => e.id_mention).HasDefaultValueSql("nextval('mentions_id_mention_seq'::regclass)");
            entity.Property(e => e.max).HasDefaultValue(20); // Valeur max par défaut
            entity.Property(e => e.min).HasDefaultValue(0);  // Valeur min par défaut
        });

        // Configuration de l'entité note et de ses relations
        modelBuilder.Entity<note>(entity =>
        {
            entity.HasKey(e => e.id_note).HasName("notes_pkey");
            entity.Property(e => e.id_note).HasDefaultValueSql("nextval('notes_id_note_seq'::regclass)");
            entity.Property(e => e.est_optionnel).HasDefaultValue(false); // Par défaut, la note n'est pas optionnelle
            entity.HasOne(d => d.id_bachelierNavigation).WithMany(p => p.notes)
                .HasForeignKey(d => d.id_bachelier)
                .HasConstraintName("notes_id_bachelier_fkey");
            entity.HasOne(d => d.id_matiereNavigation).WithMany(p => p.notes)
                .HasForeignKey(d => d.id_matiere)
                .HasConstraintName("notes_id_matiere_fkey");
        });

        // Configuration de l'entité option
        modelBuilder.Entity<option>(entity =>
        {
            entity.HasKey(e => e.id_option).HasName("options_pkey");
            entity.Property(e => e.id_option).HasDefaultValueSql("nextval('options_id_optioin_seq'::regclass)");
        });

        // Configuration de l'entité personne
        modelBuilder.Entity<personne>(entity =>
        {
            entity.HasKey(e => e.id_personne).HasName("personnes_pkey");
            entity.Property(e => e.id_personne).HasDefaultValueSql("nextval('personnes_id_personne_seq'::regclass)");
        });

        // Configuration de l'entité province
        modelBuilder.Entity<province>(entity =>
        {
            entity.HasKey(e => e.id_province).HasName("provinces_pkey");
            entity.Property(e => e.id_province).HasDefaultValueSql("nextval('provinces_id_province_seq'::regclass)");
        });

        // Déclaration des séquences utilisées pour les clés primaires
        modelBuilder.HasSequence<int>("administration_id_seq");
        modelBuilder.HasSequence<int>("bacheliers_id_bachelier_seq");
        modelBuilder.HasSequence<int>("centres_id_centre_seq");
        modelBuilder.HasSequence<int>("etablissements_id_etablissement_seq");
        modelBuilder.HasSequence<int>("historique_id_seq");
        modelBuilder.HasSequence<int>("matieres_id_matiere_seq");
        modelBuilder.HasSequence<int>("mentions_id_mention_seq");
        modelBuilder.HasSequence<int>("notes_id_note_seq");
        modelBuilder.HasSequence<int>("options_id_optioin_seq");
        modelBuilder.HasSequence<int>("personnes_id_personne_seq");
        modelBuilder.HasSequence<int>("provinces_id_province_seq");

        // Appel d'une méthode partielle pour permettre une extension de la configuration
        OnModelCreatingPartial(modelBuilder);
    }

    // Méthode partielle pour permettre une extension de la configuration dans un autre fichier
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
