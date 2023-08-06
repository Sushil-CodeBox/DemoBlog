using DemoBlogCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoBlogCore.Identity
{
   
        public class ApplicationDbContext : IdentityDbContext<IdentityUser>
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //public DbSet<CommentModel> Comment { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
            }
        }
    
}
