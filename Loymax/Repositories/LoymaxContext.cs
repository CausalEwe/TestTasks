using System;
using Microsoft.EntityFrameworkCore;

namespace Loymax.Repositories
{
    public class LoymaxContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public LoymaxContext(DbContextOptions<LoymaxContext> options)
            : base(options)
        {
        }

        public LoymaxContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=loymax;user=root;password=root;", new MySqlServerVersion(new Version(5, 7)));
        }
    }
}
