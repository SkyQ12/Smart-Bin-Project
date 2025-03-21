
namespace SmartBin.Infrastructure.Domain.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bin> Bins { get; set; }
        public DbSet<BinUnit> BinUnits { get; set; }
        public DbSet<CollectedHistory> CollectedHistories { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<PointChangedHistory> PointChangedHistories { get; set; }
        public DbSet<ErrorHistory> ErrorHistories {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BinEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BinUnitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CollectedHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LoginHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PointChangedHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorHistoryEntityTypeConfiguration());
        }
    }
}
