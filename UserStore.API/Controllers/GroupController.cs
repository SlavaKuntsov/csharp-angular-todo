using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserStore.API.Contracts;
using UserStore.Application.Services;
using UserStore.Core.Abstractions;


namespace UserStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupsResponse>>> GetGroups()
        {
            var groups = await _groupService.GetAllGroups();

            try
            {
                var response = groups.Select(g => new GroupsResponse(g.Id, g.UserId, g.Title!));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<List<UsersRequest>>> CreateUsers([FromBody] GroupsRequest request)
        {
            var group = Core.Models.Group.Create(
                request.userId,
                request.title
            );

            if (group.IsFailure)
            {
                return BadRequest(group.Error);
            }

            Console.WriteLine("done 1");

            var groupResultId = await _groupService.CreateGroup(group.Value);

            Console.WriteLine("------------groupId" + groupResultId.Value);

            if (groupResultId.IsFailure)
            {
                return BadRequest(groupResultId.Error);
            }

            return Ok(groupResultId.Value);
        }
    }
}
