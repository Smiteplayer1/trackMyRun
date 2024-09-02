using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace trackMyRun.DbEntities;

public partial class TrackMyRunContext : DbContext
{
    public TrackMyRunContext()
    {
    }

    public TrackMyRunContext(DbContextOptions<TrackMyRunContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Run> Runs { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PRIMARY");

            entity.ToTable("note");

            entity.HasIndex(e => e.RunId, "run_id_idx");

            entity.Property(e => e.NoteId).HasColumnName("note_id");
            entity.Property(e => e.NoteText)
                .HasMaxLength(255)
                .HasColumnName("note_text");
            entity.Property(e => e.RunId).HasColumnName("run_id");

            entity.HasOne(d => d.Run).WithMany(p => p.Notes)
                .HasForeignKey(d => d.RunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("run_id");
        });

        modelBuilder.Entity<Run>(entity =>
        {
            entity.HasKey(e => e.RunId).HasName("PRIMARY");

            entity.ToTable("runs");

            entity.HasIndex(e => e.ShoeId, "shoe_id_idx");

            entity.Property(e => e.RunId).HasColumnName("run_id");
            entity.Property(e => e.AvgPace)
                .HasMaxLength(255)
                .HasColumnName("avg_pace");
            entity.Property(e => e.Distance).HasColumnName("distance");
            entity.Property(e => e.HeartRate).HasColumnName("heart_rate");
            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.Time)
                .HasMaxLength(255)
                .HasColumnName("time");

            entity.HasOne(d => d.Shoe).WithMany(p => p.Runs)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shoe_id");
        });

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.HasKey(e => e.ShoeId).HasName("PRIMARY");

            entity.ToTable("shoe");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.ShoeImg)
                .HasMaxLength(255)
                .HasColumnName("shoe_img");
            entity.Property(e => e.ShoeName)
                .HasMaxLength(255)
                .HasColumnName("shoe_name");
            entity.Property(e => e.Size)
                .HasPrecision(4, 1)
                .HasColumnName("size");
            entity.Property(e => e.Width)
                .HasMaxLength(255)
                .HasColumnName("width");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
