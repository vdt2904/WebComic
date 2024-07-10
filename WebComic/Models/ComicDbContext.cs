using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebComic.Models
{
    public partial class ComicDbContext : DbContext
    {
        public ComicDbContext()
        {
        }

        public ComicDbContext(DbContextOptions<ComicDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chapter> Chapters { get; set; } = null!;
        public virtual DbSet<ChapterImage> ChapterImages { get; set; } = null!;
        public virtual DbSet<Comic> Comics { get; set; } = null!;
        public virtual DbSet<ComicGenre> ComicGenres { get; set; } = null!;
        public virtual DbSet<ComicTag> ComicTags { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<CommentsChapter> CommentsChapters { get; set; } = null!;
        public virtual DbSet<Favourite> Favourites { get; set; } = null!;
        public virtual DbSet<ForgetPassword> ForgetPasswords { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServicePrice> ServicePrices { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<UseService> UseServices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<View> Views { get; set; } = null!;

/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PREC5540;Database=ComicDb;Trusted_Connection=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.ComicId).HasColumnName("ComicID");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.ComicId)
                    .HasConstraintName("FK__Chapters__ComicI__3B75D760");
            });

            modelBuilder.Entity<ChapterImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__ChapterI__7516F4ECFBCA7A01");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.ChapterImages)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK__ChapterIm__Chapt__3E52440B");
            });

            modelBuilder.Entity<Comic>(entity =>
            {
                entity.Property(e => e.ComicId).HasColumnName("ComicID");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CoverImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.PublishDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ComicGenre>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ComicId).HasColumnName("ComicID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.ComicGenres)
                    .HasForeignKey(d => d.ComicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComicGenr__Comic__4316F928");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.ComicGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComicGenr__Genre__440B1D61");
            });

            modelBuilder.Entity<ComicTag>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ComicId).HasColumnName("ComicID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.ComicTags)
                    .HasForeignKey(d => d.ComicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComicTags__Comic__48CFD27E");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ComicTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComicTags__TagID__49C3F6B7");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ComicId).HasColumnName("ComicID");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ComicId)
                    .HasConstraintName("FK__Comments__ComicI__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__UserID__4CA06362");
            });

            modelBuilder.Entity<CommentsChapter>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("CommentsChapter");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.CommentsChapters)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK_CommentsChapter_Chapters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentsChapters)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CommentsChapter_Users");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.ToTable("Favourite");

                entity.Property(e => e.Datelike)
                    .HasColumnType("datetime")
                    .HasColumnName("datelike");

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.ComicId)
                    .HasConstraintName("FK_Favourite_Comics");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Favourite_Users");
            });

            modelBuilder.Entity<ForgetPassword>(entity =>
            {
                entity.ToTable("ForgetPassword");

                entity.Property(e => e.DateExprired).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(6);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ForgetPasswords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ForgetPassword_Users");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RefreshToken_Users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceName).HasMaxLength(50);
            });

            modelBuilder.Entity<ServicePrice>(entity =>
            {
                entity.ToTable("ServicePrice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ServicePrice1).HasColumnName("ServicePrice");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicePrices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ServicePrice_Service");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UseService>(entity =>
            {
                entity.ToTable("UseService");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateExprired).HasColumnType("datetime");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.UseServices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_UseService_Service");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UseServices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UseService_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<View>(entity =>
            {
                entity.Property(e => e.Dateview).HasColumnType("datetime");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK_Views_Chapters");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Views_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
