using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalkForum.Domain.Entities;

namespace WalkForum.Infrastructure.Persistance;

internal class ForumDbContext(DbContextOptions<ForumDbContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    internal DbSet<Post> Posts { get; set; }
    internal DbSet<Tag> Tags { get; set; }
    internal DbSet<Message> Messages { get; set; }
    internal DbSet<Category> Categories { get; set; }
    //internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.Author)
            .HasForeignKey(e => e.AuthorId)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasMany(e => e.Messages)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .IsRequired();


        modelBuilder.Entity<User>()
            .HasMany(e => e.Messages)
            .WithOne(e => e.User)
            .HasForeignKey(e =>e.UserId)
            .IsRequired();

    }
}
