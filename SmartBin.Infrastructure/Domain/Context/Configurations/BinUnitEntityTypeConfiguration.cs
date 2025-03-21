
namespace SmartBin.Infrastructure.Domain.Context.Configurations
{
    public class BinUnitEntityTypeConfiguration : IEntityTypeConfiguration<BinUnit>
    {
        public void Configure(EntityTypeBuilder<BinUnit> builder)
        {
            builder.HasKey(x => x.BinUnitId);
            builder.Property(x => x.BinUnitId).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasMany(x => x.CollectedHistories).WithOne(x => x.BinUnit).HasForeignKey(x => x.BinUnitId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ErrorHistories).WithOne(x => x.BinUnit).HasForeignKey(x => x.BinUnitId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
