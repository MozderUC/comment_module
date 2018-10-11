using comment_system_01.Models;
using comment_system_01.Models.Account;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace comment_system_01.DAL
{
    public class MovieDBContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDBContext() : base("MovieDBContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Upvote> Upvote { get; set; }
        public DbSet<Image> Image { get; set; }

        public static MovieDBContext Create()
        {
            return new MovieDBContext();
        }

        


    }
}