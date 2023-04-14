using ITE5331FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITE5331FinalProject.Data
{
    public class ForumContext : IdentityDbContext<IdentityUser>
    {
        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        { }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasData(
                new Article
                {
                    ArticleId = 1,
                    Title = "Amazing idea",
                    Author = "Zhangzhi Wang",
                    PublishTime = DateTime.Parse("2023-01-10 10:30:00"),
                    NumViews = 10,
                    NumLikes = 2,
                    Tag = Tag.Essence,
                    Text = "An amazing article"
                },
                new Article
                {
                    ArticleId = 2,
                    Title = "How to change the world",
                    Author = "Zhangzhi Wang",
                    PublishTime = DateTime.Parse("2023-01-10 11:30:00"),
                    NumViews = 5,
                    NumLikes = 2,
                    Tag = Tag.Essence,
                    Text = "This is a article about how to change the world"
                }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentId = 1,
                    ArticleId = 1,
                    Author = "Elon Musk",
                    PublishTime = DateTime.Parse("2023-01-10 12:30:00"),
                    Text = "Great article, I learned a lot!",
                    NumLikes = 5
                },
                new Comment
                {
                    CommentId = 2,
                    ArticleId = 1,
                    Author = "Bill Gates",
                    PublishTime = DateTime.Parse("2023-01-10 13:30:00"),
                    Text = "Interesting perspective, thank you for sharing!",
                    NumLikes = 3
                },
                new Comment
                {
                    CommentId = 3,
                    ArticleId = 2,
                    Author = "Elon Musk",
                    PublishTime = DateTime.Parse("2023-01-10 14:30:00"),
                    Text = "This is inspiring, let's change the world together!",
                    NumLikes = 10
                },
                new Comment
                {
                    CommentId = 4,
                    ArticleId = 2,
                    Author = "Bill Gates",
                    PublishTime = DateTime.Parse("2023-01-10 15:30:00"),
                    Text = "I agree, we need to take action and make a difference.",
                    NumLikes = 7
                }
            );

            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.Author)
            //    .WithMany()
            //    .HasForeignKey(c => c.AuthorId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
