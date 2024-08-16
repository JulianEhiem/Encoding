using System;
using System.Collections.Generic;
using Encoding.Models;
using Microsoft.EntityFrameworkCore;

namespace Encoding.Data;

public partial class EncodingContext : DbContext
{
    public EncodingContext()
    {
    }

    public EncodingContext(DbContextOptions<EncodingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Problem> Problems { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Encoding");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Problem>(entity =>
        {
            entity.HasKey(e => e.ProblemId).HasName("Problem_pk");

            entity.ToTable("Problem");

            entity.HasIndex(e => e.ProblemNumber, "Problem_pk_2").IsUnique();

            entity.Property(e => e.ProblemId).HasColumnName("problem_id");
            entity.Property(e => e.ProblemDescription)
                .IsUnicode(false)
                .HasColumnName("problem_description");
            entity.Property(e => e.ProblemNumber).HasColumnName("problem_number");
            entity.Property(e => e.ProblemRatingId).HasColumnName("problem_rating_id");
            entity.Property(e => e.ProblemTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("problem_title");

            entity.HasOne(d => d.ProblemRating).WithMany(p => p.Problems)
                .HasForeignKey(d => d.ProblemRatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Problem_Rating_rating_id_fk");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("Rating_pk");

            entity.ToTable("Rating");

            entity.Property(e => e.RatingId)
                .ValueGeneratedNever()
                .HasColumnName("rating_id");
            entity.Property(e => e.RatingName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rating_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
