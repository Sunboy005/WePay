using AutoMapper;
using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wepay.Models.DTOs;
using wepay.Service.Interface;


namespace wepay.Controllers
{
    [Route("wepay/user")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly IServiceManager _serviceManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserController(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        [HttpGet("id", Name = "GetUserById")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            var user = await _serviceManager.UserService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var role = await _serviceManager.UserService.GetRoleOfUser(user);

            var identityUser = _mapper.Map<IdentityUserDto>(user);
            identityUser.Role = role;
            return Ok(identityUser);
        }

        [HttpGet("email", Name = "GetUserByEmail")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {

            var user = await _serviceManager.UserService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            var role = await _serviceManager.UserService.GetRoleOfUser(user);

            var identityUser = _mapper.Map<IdentityUserDto>(user);
            identityUser.Role = role;
            return Ok(identityUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("upgrade_user/{userId}")]

        public async Task<IActionResult> UpgradeUser(string userId)
        {
            var user = await _serviceManager.UserService.GetUserById(userId);
            if(user == null)
            {
                throw new NotFoundException("User not found");
            }
            await _serviceManager.UserService.UpgradeUser(user);
            return Ok();

        }
        

        [HttpPost("create-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _serviceManager.UserService.RegisterUser(userForRegistrationDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);

        }


        [HttpDelete("delete-user")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromBody] UserDeletionDto userDeletionDto)
        {
            await _serviceManager.UserService.DeleteUser(userDeletionDto);
            return NoContent();
        }

        [HttpPatch("update-user/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto userUpdateDto)
        {
            await _serviceManager.UserService.UpdateUserAsync(id, userUpdateDto);

            return NoContent();
        }


        [HttpPost("create-admin")]
        [Authorize]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminForRegistrationDto adminForRegistrationDto)
        {
            var result = await _serviceManager.UserService.RegisterAdmin(adminForRegistrationDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);

        }
    }
}

