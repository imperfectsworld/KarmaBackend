using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Karma_Backend.Models;

public partial class KarmaDbContext : DbContext
{
    public KarmaDbContext()
    {
    }

    public KarmaDbContext(DbContextOptions<KarmaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=karmabook.database.windows.net;Initial Catalog=KarmaDb;User Id=Karma;Password=ADD$$devs;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.CommunityId).HasName("PK__Communit__9381304DFD950ABC");

            entity.ToTable("Community");

            entity.Property(e => e.CommunityId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("communityId");
            entity.Property(e => e.ZipCode).HasColumnName("zipCode");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3213E83F8B26C1D2");

            entity.ToTable("Item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categories)
                .HasMaxLength(1000)
                .HasColumnName("categories");
            entity.Property(e => e.Condition)
                .HasMaxLength(255)
                .HasColumnName("condition");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("description");
            entity.Property(e => e.GeoCode)
                .HasMaxLength(1000)
                .HasColumnName("geoCode");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasColumnName("googleID");
            entity.Property(e => e.Likes).HasColumnName("likes");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Pic)
                .HasMaxLength(400)
                .HasColumnName("pic");

            entity.HasOne(d => d.Google).WithMany(p => p.Items)
                .HasForeignKey(d => d.GoogleId)
                .HasConstraintName("FK__Item__googleID__68487DD7");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3213E83F8F4A099E");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.GoogleId).HasName("PK__User__0DA2E4E3A355A57A");

            entity.ToTable("User");

            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasColumnName("googleID");
            entity.Property(e => e.CommunityId).HasColumnName("communityId");
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(255)
                .HasColumnName("profilePic");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("userName");

            entity.HasOne(d => d.Community).WithMany(p => p.Users)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK__User__communityI__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
