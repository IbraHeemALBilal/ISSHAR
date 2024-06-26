using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IInviteRepository
    {
        Task<ICollection<Invite>> GetByReceiverIdAsync(int receiverId);
        Task<Invite> GetByIdAsync(int id);
        Task AddAsync(Invite invite);
        Task<bool>CheckIfInvitedBeforeAsnyc(int senderId , int receiverId , int cardId);
    }
}
