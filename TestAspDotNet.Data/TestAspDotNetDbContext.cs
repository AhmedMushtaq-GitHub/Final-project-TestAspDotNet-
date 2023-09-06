using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Model;

namespace TestAspDotNet.Data
{
    public class TestAspDotNetDbContext : DbContext
    {
        public TestAspDotNetDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Post> Posts  { get; set; }
        public DbSet<PostComment> PostComments  { get; set; }
        public DbSet<PostReaction> PostReactions  { get; set; }
        public DbSet<PostStatus> PostStatuses  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<ReactionType> ReactionTypes  { get; set; }
    }

}
