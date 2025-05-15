
namespace SmartBin.Infrastructure.Domain.Context.Configurations
{
    public class CollectedHistoryEntityTypeConfiguration : IEntityTypeConfiguration<CollectedHistory>
    {
        public void Configure(EntityTypeBuilder<CollectedHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CollectedTime).IsRequired();
            builder.Property(x => x.BinId);
            builder.Property(x => x.BinUnitId).IsRequired(false);
            builder.Property(x => x.BinType).IsRequired(false);
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.District).IsRequired();
        }
    }
}
