using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace BookAPI.Contexts;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options)
    : base(options)
    {
    }
    public BookContext()
    {

    }

    public  DbSet<BookItem> BookItems { get; set; }
    public object BookItem { get; internal set; }

    //public object Books { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=pass;Database=BookAPI");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_pkey");

            entity.ToTable("books");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .HasColumnName("author");

            entity.Property(e => e.Genre)
                .HasMaxLength(100)
                .HasColumnName("genre");

            entity.Property(e => e.PublicationYear)
                .HasColumnName("publication_year");

            entity.Property(e => e.Publisher)
                .HasMaxLength(255)
                .HasColumnName("publisher");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .HasColumnName("isbn");


            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        });

       // OnModelCreatingPartial(modelBuilder);
    }

   // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}