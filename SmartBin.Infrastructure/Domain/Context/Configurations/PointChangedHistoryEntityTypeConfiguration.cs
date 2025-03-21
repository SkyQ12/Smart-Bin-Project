using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBin.Infrastructure.Domain.Context.Configurations
{
    public class PointChangedHistoryEntityTypeConfiguration : IEntityTypeConfiguration<PointChangedHistory>
    {
        public void Configure(EntityTypeBuilder<PointChangedHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.OldPoint).IsRequired();
            builder.Property(x => x.NewPoint).IsRequired();
            builder.Property(x => x.PointChangedTime).IsRequired();
        }
    }
}
