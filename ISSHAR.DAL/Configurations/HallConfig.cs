using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Configurations
{
        public class HallConfig : IEntityTypeConfiguration<Hall>
        {
            public void Configure(EntityTypeBuilder<Hall> builder)
            {
                builder.HasKey(a => a.HallId);
                builder.Property(a => a.HallId).ValueGeneratedOnAdd();
                builder.Property(a => a.UserId).IsRequired();
                builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
                builder.Property(a => a.location).IsRequired().HasMaxLength(255);
                builder.Property(a => a.Capacity).IsRequired();

               builder.HasOne(a => a.User)
                       .WithMany(u => u.Halls)
                       .HasForeignKey(a => a.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
               builder.HasMany(u => u.Bookings)
                  .WithOne(a => a.Hall)
                  .HasForeignKey(a => a.HallId)
                  .OnDelete(DeleteBehavior.Cascade);
               builder.HasMany(u => u.HallImages)
                .WithOne(a => a.Hall)
                .HasForeignKey(a => a.HallId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        }
    
}
