using Application.Services.OrganizationService;
using Core.DTOs.Entities;
using Core.DTOs.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrganizationsController : BaseApiController
    {
        [HttpGet("{organizationId}/members")]
        public async Task<ActionResult<List<UserListItemDto>>> GetOrganizationMembers(Guid organizationId)
        {
            return HandleResult(await Mediator.Send(new OrganizationMembersList.Query { OrganizationId = organizationId }));
        }
        [HttpPost]
        public async Task<ActionResult<OrganizationDto>> CreateOrganization(OrganizationDto organizationDto)
        {
            return HandleResult(await Mediator.Send(new CreateOrganization.Command { OrganizationDto = organizationDto }));
        }
        [HttpDelete("{organizationId}")]
        public async Task<ActionResult<Unit>> DeleteOrganization(Guid organizationId)
        {
            return HandleResult(await Mediator.Send(new DeleteOrganizationById.Command { OrganizationId = organizationId }));
        }
        [HttpGet("{organizationId}")]
        public async Task<ActionResult<OrganizationDto>> GetOrganizationDetails(Guid organizationId)
        {
            return HandleResult(await Mediator.Send(new OrganizationDetailsById.Query { OrganizationId = organizationId }));
        }
        [HttpDelete("{organizationId}/kick/{userId}")]
        public async Task<ActionResult<Unit>> KickUserFromOrganization(Guid organizationId, Guid userId)
        {
            return HandleResult(await Mediator.Send(new KickUserFromOrganization.Command { OrganizationId = organizationId, UserId = userId }));
        }
        [HttpPost("{organizationId}/append/{userId}")]
        public async Task<ActionResult<Unit>> AppendUserToOrganization(Guid organizationId, Guid userId)
        {
            return HandleResult(await Mediator.Send(new AppendUserToOrganization.Command { OrganizationId = organizationId, UserId = userId }));
        }
    }
}