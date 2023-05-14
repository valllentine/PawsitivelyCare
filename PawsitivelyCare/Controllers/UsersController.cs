using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DTOs.User;
using System.Security.Claims;

namespace PawsitivelyCare.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            var user = await _userService.Get(UserId);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto userDto)
        {
            var userModel = _mapper.Map<UserModel>(userDto);
            var user = await _userService.AuthenticateUser(userModel);

            if (user != null)
            {
                var token = _userService.GenerateJwtToken(user);

                return Ok(new { token });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDto userDto)
        {
            var userModel = _mapper.Map<UserModel>(userDto);
            var createdUser = await _userService.Register(userModel);
            return Ok(new { message = "Registration successful", createdUser });
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] UpdateUserDto userDto)
        {
            var userModel = await _userService.Get(UserId);

            if (userModel == null)
                return NotFound();

            _mapper.Map(userDto, userModel);
            await _userService.Update(userModel);

            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
