using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Configurations
{
        public class HallConfig : IEntityTypeConfiguration<Hall>
        {
            public void Configure(EntityTypeBuilder<Hall> builder)
            {
                builder.HasKey(h => h.HallId);
                builder.Property(h => h.HallId).IsRequired().ValueGeneratedOnAdd();
                builder.Property(h => h.OwnerId).IsRequired();
                builder.Property(h => h.Name).IsRequired().HasMaxLength(255);
                builder.Property(h => h.Description).IsRequired().HasMaxLength(255);
                builder.Property(h => h.City).IsRequired().HasMaxLength(255);
                builder.Property(h => h.Address).IsRequired().HasMaxLength(255);
                builder.Property(h => h.Logo).IsRequired().HasMaxLength(255);
                builder.Property(h => h.Capacity).IsRequired();
                builder.Property(h => h.PartyPrice).IsRequired();
                builder.Property(h=> h.Status).IsRequired().HasMaxLength(20);

            builder.HasOne(h => h.Owner)
                       .WithMany(u => u.Halls)
                       .HasForeignKey(a => a.OwnerId)
                       .OnDelete(DeleteBehavior.Cascade);
               builder.HasMany(h => h.Bookings)
                  .WithOne(b => b.Hall)
                  .HasForeignKey(b => b.HallId)
                  .OnDelete(DeleteBehavior.NoAction);
               builder.HasMany(h => h.HallImages)
                .WithOne(hi => hi.Hall)
                .HasForeignKey(hi => hi.HallId)
                .OnDelete(DeleteBehavior.Cascade);
            }
        }
    
}
