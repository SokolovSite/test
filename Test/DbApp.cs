using Microsoft.EntityFrameworkCore;


namespace Test
{
    public class DbApp : DbContext
    {
        public DbApp(DbContextOptions<DbApp> options) : base(options)
        {

        }

        public DbSet<Obj> DbObjects { get; set; }
        public DbApp() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postest;Username=postgres;Password=Sokolov510;");
        }
    }
}
