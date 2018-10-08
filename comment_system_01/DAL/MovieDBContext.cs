using comment_system_01.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace comment_system_01.DAL
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext() : base("MovieDBContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Upvote> Upvote { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}