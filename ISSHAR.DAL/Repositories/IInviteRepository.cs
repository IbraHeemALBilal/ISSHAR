using ISSHAR.DAL.Entities;

namespace ISSHAR.DAL.Repositories
{
    public interface IInviteRepository
    {
        Task<Invite> GetByIdAsync(int id);//include user
        Task<ICollection<Hall>> GetByReceiverIdAsync(int receiverId);//include user
        Task AddAsync(Invite invite);
        Task<bool>CheckIfInvitedBeforAsnyc(int senderId , int receiverId , int cardId);
    }
}
