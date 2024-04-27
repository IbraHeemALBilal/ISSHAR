using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISSHAR.DAL.Configurations
{
    public class InviteConfig : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.HasKey(invite => invite.InviteId);

            builder.Property(invite => invite.InviteId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(invite => invite.Sender)
                   .WithMany(user => user.SendedInvites)
                   .HasForeignKey(invite => invite.SenderId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(invite => invite.Receiver)
                   .WithMany(user => user.ReceivedInvites)
                   .HasForeignKey(invite => invite.ReceiverId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(invite => invite.Card)
                   .WithMany(card => card.Invites)
                   .HasForeignKey(invite => invite.CardId)
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
