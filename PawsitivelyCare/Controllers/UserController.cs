using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;

namespace PawsitivelyCare.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<ActionResult> GetUser([FromQuery] int id)
        {
            var user = await _userService.Get(id);
            return Ok(user);
        }

        //[HttpPost]
        //public async Task<ActionResult<int>> AddUser([FromBody] CreateUserDto userDto)
        //{
        //    var userModel = _mapper.Map<UserModel>(userDto);
        //    var createdAddress = await _userService.Add(userModel);
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
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
