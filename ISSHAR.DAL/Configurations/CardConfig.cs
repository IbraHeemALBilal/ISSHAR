using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(e => e.CardId);
            builder.Property(e => e.CardId).IsRequired().ValueGeneratedOnAdd();

            builder.Property(e => e.PartyDate).IsRequired();

            builder.Property(e => e.JsonData).IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
