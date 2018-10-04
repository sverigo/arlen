using Microsoft.EntityFrameworkCore;


namespace arlen.Models
{
    public class ArlenContext : DbContext
    {
        //public ArlenContext() : base("ArlenDatabase") { }

        public DbSet<News> NewsList { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Partner> Partners { get; set; }
        /*
        static ArlenContext()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public static ArlenContext Create()
        {
            return new ArlenContext();
        }*/
        public ArlenContext(DbContextOptions<ArlenContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
    /*
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ArlenContext>
    {
        protected override void Seed(ArlenContext context)
        {
            InitialSetup(context);
            base.Seed(context);
        }

        public void InitialSetup(ArlenContext context)
        {
            

            context.SaveChanges();
        }
    }*/
}