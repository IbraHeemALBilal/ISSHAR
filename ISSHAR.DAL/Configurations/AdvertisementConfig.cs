using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISSHAR.DAL.Configurations
{
    public class AdvertisementConfig : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasKey(a => a.AdvertisementId);
            builder.Property(a => a.AdvertisementId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(255);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.ImageUrl).HasMaxLength(255);
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(10);
            builder.Property(a => a.DatePosted).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.City).HasMaxLength(20);
            builder.Property(a => a.ServiceType).IsRequired().HasMaxLength(25);
            builder.Property(a => a.Status).IsRequired().HasMaxLength(20);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Advertisements)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
