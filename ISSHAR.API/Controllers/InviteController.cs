using ISSHAR.Application.DTOs.InviteDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("api/invites")]
    [ApiController]
    [Authorize(Roles = "Reguler, HallOwner")]
    public class InviteController : ControllerBase
    {
        private readonly IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpGet("receiver/{receiverId}")]
        [UserIdAuthorization("receiverId")]
        public async Task<ActionResult<ICollection<InviteDisplayDTO>>> GetByReceiverIdAsync(int receiverId)
        {
            var inviteDTOs = await _inviteService.GetByReceiverIdAsync(receiverId);
            return Ok(inviteDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InviteDisplayDTO>> GetByIdAsync(int id)
        {
            var inviteDTO = await _inviteService.GetByIdAsync(id);
            if (inviteDTO == null)
            {
                return NotFound();
            }
            return Ok(inviteDTO);
        }

        [HttpPost]
        public async Task<ActionResult> SentInviteAsync(InviteDTO inviteDTO)
        {
            var result = await _inviteService.SentInviteAsync(inviteDTO);
            if (result)
            {
                return Ok();
            }
            return Conflict("Invite has already been sent before.");
        }
    }
}
