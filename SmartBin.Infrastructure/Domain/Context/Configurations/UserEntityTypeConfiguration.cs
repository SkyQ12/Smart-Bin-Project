
namespace SmartBin.Infrastructure.Domain.Context.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.UserName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Point);
            builder.Property(x => x.IdentificationNumber);
            builder.Property(x => x.Sex);
            builder.Property(x => x.Birthday);
            builder.Property(x => x.HomeTown);
            builder.Property(x => x.IssuanceDate);
            builder.Property(x => x.Role).IsRequired();

            builder.HasMany(x => x.LoginHistories).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.PointChangedHistories).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
