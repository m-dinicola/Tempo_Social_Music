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
                entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");

                entity.HasOne(d => d.User1Navigation)
                    .WithMany(p => p.ConnectionUser1Navigation)
                    .HasForeignKey(d => d.User1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Connectio__User1__6EF57B66");

                entity.HasOne(d => d.User2Navigation)
                    .WithMany(p => p.ConnectionUser2Navigation)
                    .HasForeignKey(d => d.User2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Connectio__User2__6FE99F9F");
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

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favorites__UserI__70DDC3D8");
            });

            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.Property(e => e.RecommendationId).HasColumnName("RecommendationID");

                entity.Property(e => e.RecSongId).HasColumnName("RecSongID");

                entity.HasOne(d => d.UserFromNavigation)
                    .WithMany(p => p.RecommendationUserFromNavigation)
                    .HasForeignKey(d => d.UserFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recommend__UserF__72C60C4A");

                entity.HasOne(d => d.UserToNavigation)
                    .WithMany(p => p.RecommendationUserToNavigation)
                    .HasForeignKey(d => d.UserTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recommend__UserT__73BA3083");
            });

            modelBuilder.Entity<TempoUser>(entity =>
            {
                entity.HasKey(e => e.UserPk)
                    .HasName("PK__TempoUse__1788857E33983D41");

                entity.Property(e => e.UserPk).HasColumnName("UserPK");

                entity.Property(e => e.AspNetUserId)
                    .HasColumnName("AspNetUserID")
                    .HasMaxLength(450)
                    .IsUnicode(false);

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
