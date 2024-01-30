using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserStore.API.Contracts;
using UserStore.Application.Services;
using UserStore.Core.Abstractions;

namespace UserStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemsResponse>>> GetItems()
        {
            var groups = await _itemService.GetAllItems();

            try
            {
                var response = groups.Select(i => new ItemsResponse(i.Id, i.GroupId, i.Title!, i.Description, i.Status));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<ActionResult<List<ItemsRequest>>> CreateUsers([FromBody] ItemsRequest request)
        {
            var group = Core.Models.Item.Create(
                request.groupId,
                request.title,
                request.description,
                request.status
            );

            if (group.IsFailure)
            {
                return BadRequest(group.Error);
            }

            Console.WriteLine("done 1");

            var groupResultId = await _itemService.CreateItem(group.Value);

            Console.WriteLine("------------groupId" + groupResultId.Value);

            if (groupResultId.IsFailure)
            {
                return BadRequest(groupResultId.Error);
            }

            return Ok(groupResultId.Value);
        }
    }
}
