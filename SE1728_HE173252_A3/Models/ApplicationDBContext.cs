using Microsoft.EntityFrameworkCore;
using System;

namespace SE1728_HE173252_A3.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(conf.GetConnectionString("DBContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorID);

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PostCategory>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryID);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostID);
                entity.Property(e => e.PostID).ValueGeneratedOnAdd();
            });

            // Seed data with explicit CategoryID values
            modelBuilder.Entity<PostCategory>().HasData(
                new PostCategory { CategoryID = 1, CategoryName = "Technology", Description = "Technology related posts" },
                new PostCategory { CategoryID = 2, CategoryName = "Finance", Description = "Finance related posts" },
                new PostCategory { CategoryID = 3, CategoryName = "Education", Description = "Education related posts" },
                new PostCategory { CategoryID = 4, CategoryName = "Travel", Description = "Travel related posts" }
            );
        }

    }
}
