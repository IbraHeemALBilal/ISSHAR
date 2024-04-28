using AutoMapper;
using ISSHAR.Application.DTOs.InviteDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace ISSHAR.Application.Services
{
    public class InviteService : IInviteService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IInviteService> _logger;

        public InviteService(IInviteRepository inviteRepository, IMapper mapper, ILogger<IInviteService> logger)
        {
            _inviteRepository = inviteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<InviteDisplayDTO> GetByIdAsync(int id)
        {
            try
            {
                var invite =await _inviteRepository.GetByIdAsync(id);
                var inviteDTO = _mapper.Map<InviteDisplayDTO>(invite);
                return inviteDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Invite by ID: {InviteID}", id);
                throw;
            }
        }

        public async Task<ICollection<InviteDisplayDTO>> GetByReceiverIdAsync(int receiverId)
        {
            try
            {
                var invites = await _inviteRepository.GetByReceiverIdAsync(receiverId);
                var inviteDTOs = _mapper.Map<ICollection<InviteDisplayDTO>>(invites);
                return inviteDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Invites by receiverId.");
                throw;
            }
        }

        public async Task<bool> SentInviteAsync(InviteDTO inviteDTO)
        {
            try
            {
                var sentBefore = await _inviteRepository.CheckIfInvitedBeforeAsnyc(inviteDTO.SenderId, inviteDTO.ReceiverId, inviteDTO.CardId);
                if (sentBefore)
                {
                    return false;
                }
                var invite = _mapper.Map<Invite>(inviteDTO);
                await _inviteRepository.AddAsync(invite);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while senting Invite.");
                throw;
            }

        }
    }
}
