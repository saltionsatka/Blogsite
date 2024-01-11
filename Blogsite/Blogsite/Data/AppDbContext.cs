using System;
using System.Diagnostics;
using Blogsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogsite.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Name = "User", NormalizedName = "USER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );


            //modelBuilder.Entity<CategoryPost>().HasKey(pc => new { pc.PostsId, pc.CategoriesId });
            //modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostsId, pt.TagsId });
            modelBuilder.Entity<Comment>()
            .HasOne<Post>(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(p => p.PostId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<CategoryPost> PostCategories { get; set; }
        //public DbSet<PostTag> PostTags { get; set; }


    }
}
