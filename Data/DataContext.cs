
using Microsoft.EntityFrameworkCore;
using seventh.Models;

namespace seventh.Data
{

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Server> Server { get; set; }
        public DbSet<Videos> Videos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}