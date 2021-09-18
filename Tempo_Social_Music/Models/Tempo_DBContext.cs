using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tempo_Social_Music.Models
{
    public partial class Tempo_DBContext : DbContext
    {
        //private readonly string _connection;
        public Tempo_DBContext()
        {
        }

        public Tempo_DBContext(DbContextOptions<Tempo_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Connection> Connection { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Recommendation> Recommendation { get; set; }
        public virtual DbSet<TempoUser> TempoUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ConnectionId)
                    .HasColumnName("ConnectionID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.HasKey(e => e.Favorite)
                    .HasName("PK__Favorite__54FC60F165C90270");

                entity.Property(e => e.SpotArtist)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SpotTrack)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.Property(e => e.RecommendationId).HasColumnName("RecommendationID");

                entity.Property(e => e.RecSongId).HasColumnName("RecSongID");
            });

            modelBuilder.Entity<TempoUser>(entity =>
            {
                entity.HasKey(e => e.UserPk)
                    .HasName("PK__TempoUse__1788857E33983D41");

                entity.Property(e => e.UserPk).HasColumnName("UserPK");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(40);

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.State).HasMaxLength(25);

                entity.Property(e => e.StreetAddress).HasMaxLength(100);

                entity.Property(e => e.UserBio).HasMaxLength(500);

                entity.Property(e => e.ZipCode).HasMaxLength(6);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
