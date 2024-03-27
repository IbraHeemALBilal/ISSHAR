using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Configurations
{
        public class BookingConfig : IEntityTypeConfiguration<Booking>
        {
            public void Configure(EntityTypeBuilder<Booking> builder)
            {
                builder.HasKey(a => a.BookingId);
                builder.Property(a => a.BookingId).ValueGeneratedOnAdd();
                builder.Property(a => a.HallId).IsRequired();
                builder.Property(a => a.UserId).IsRequired();
                builder.Property(a => a.StartDate).IsRequired().HasDefaultValueSql("GETDATE()");
                builder.Property(a => a.EndDate);

                builder.HasOne(a => a.User)
                       .WithMany(u => u.Bookings)
                       .HasForeignKey(a => a.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
                builder.HasOne(a => a.Hall)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(a => a.HallId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
        }
}
