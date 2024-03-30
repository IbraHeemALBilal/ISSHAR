using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISSHAR.DAL.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.DateOfBirth).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Gender).IsRequired();
            builder.Property(u => u.Role).IsRequired().HasMaxLength(20);

            builder.HasMany(u => u.Bookings)
                  .WithOne(b => b.User)
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
