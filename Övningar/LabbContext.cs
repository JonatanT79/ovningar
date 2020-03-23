using Microsoft.EntityFrameworkCore;

namespace Övningar
{
    class LabbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\Mssqllocaldb; Database= ÖvningDB; MultipleActiveResultSets=true");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Sport> Sports { get; set; }
    }
}
