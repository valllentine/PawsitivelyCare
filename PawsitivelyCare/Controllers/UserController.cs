using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DTOs.User;

namespace PawsitivelyCare.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser([FromQuery] Guid id)
        {
            var user = await _userService.Get(id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserDto userDto)
        {
            var userModel = _mapper.Map<UserModel>(userDto);
            var createdUser = await _userService.Register(userModel);
            return Ok(new { message = "Registration successful", createdUser });
        }

        //[HttpPost]
        //public async Task<ActionResult<int>> AddUser([FromBody] CreateUserDto userDto)
        //{
        //    var userModel = _mapper.Map<UserModel>(userDto);
        //    var createdAddress = await _userService.Register(userModel);
        //    return Ok(createdAddress.Id);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        //{
        //    var userModel = await _userService.Get(id);

        //    if (userModel == null)
        //        return NotFound();

        //    _mapper.Map(userDto, userModel);
        //    await _userService.Update(userModel);
        //    return Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
