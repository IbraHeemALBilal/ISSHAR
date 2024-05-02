using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Configurations
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
        {
            public void Configure(EntityTypeBuilder<Booking> builder)
            {
                builder.HasKey(b => b.BookingId);
                builder.Property(b => b.BookingId).IsRequired().ValueGeneratedOnAdd();
                builder.Property(b => b.StartDate).IsRequired();
                builder.Property(b => b.EndDate).IsRequired();
            }
        }
}
