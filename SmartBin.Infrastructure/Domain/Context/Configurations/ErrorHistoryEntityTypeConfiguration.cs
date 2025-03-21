
namespace SmartBin.Infrastructure.Domain.Context.Configurations
{
    public class ErrorHistoryEntityTypeConfiguration : IEntityTypeConfiguration<ErrorHistory>
    {
        public void Configure(EntityTypeBuilder<ErrorHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.ErrorName).IsRequired();
            builder.Property(x => x.TimeStamp).IsRequired();
        }
    }
}
