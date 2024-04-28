using ISSHAR.Application.DTOs.InviteDTOs;

namespace ISSHAR.Application.Services
{
    public interface IInviteService
    {
        Task<ICollection<InviteDisplayDTO>>GetByReceiverIdAsync(int receiverId);
        Task<InviteDisplayDTO> GetByIdAsync(int id);
        Task<bool> SentInviteAsync(InviteDTO inviteDTO);
    }
}
