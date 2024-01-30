using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UserStore.API.Contracts;
using UserStore.Core.Abstractions;
using UserStore.Core.Models;


namespace UserStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetUsers()
        {

            var users = await _userService.GetAllUsers();

            try
            {
                var response = users.Select(u => new UsersResponse(u.Id, u.Email!, u.Password!, u.Token!));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<List<UsersRequest>>> CreateUsers([FromBody] UsersRequest request)
        {

            var user = Core.Models.User.Create(
                request.email,
                request.password
            );

            if (user.IsFailure)
            {
                return BadRequest(user.Error);
            }

            var userToken = await _userService.CreateUser(user.Value);

            string jsonUserToken = JsonSerializer.Serialize(userToken);

            return Ok(jsonUserToken);
        }

        [HttpPost("Login")]
        public ActionResult<List<UsersRequest>> LoginUsers([FromBody] UsersRequestLogin request)
        {
            var user = Core.Models.User.Create(
                request.email,
                request.password,
                request.token
            );

            if (user.IsFailure)
            {
                return BadRequest(user.Error);
            }

            var loginResult = _userService.LoginUser(user.Value); 

            if(loginResult.IsFailure)
            {
                return BadRequest(loginResult.Error);
            }

            return Ok(loginResult.Value);

        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UsersRequest request)
        {
            var userUd = await _userService.UpdateUser(
                id,
                request.email,
                request.password);

            return Ok(userUd);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid id)
        {
            return Ok(await _userService.DeleteUser(id));
        }
    }
}
