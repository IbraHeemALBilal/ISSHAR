using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISSHAR.DAL.Configurations
{
    public class CardTempletConfig : IEntityTypeConfiguration<CardTemplet>
    {
        public void Configure(EntityTypeBuilder<CardTemplet> builder)
        {
            builder.HasKey(e => e.CardTempletId);
            builder.Property(e => e.CardTempletId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.Property(e => e.JsonData).IsRequired();

        }
    }
}
