
namespace SmartBin.Infrastructure.Domain.Context.Configurations
{
    public class BinEntityTypeConfiguration : IEntityTypeConfiguration<Bin>
    {
        public void Configure(EntityTypeBuilder<Bin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Longtitude);
            builder.Property(x => x.Latitude);
            builder.Property(x => x.Address);
            builder.Property(x => x.Battery);
            builder.Property(x => x.Internet);
            builder.Property(x => x.Qr);
            builder.HasMany(x => x.BinUnits).WithOne(x => x.Bin).HasForeignKey(x => x.BinId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
