namespace PressYourLuckApi.Data
{
    public partial class ContextPYL : DbContext
    {
        public ContextPYL() { }

        public ContextPYL(DbContextOptions<ContextPYL> options)
            : base(options) { }

        public virtual DbSet<Scoring> Scoring { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // nothing here for now
        }
    }
}
