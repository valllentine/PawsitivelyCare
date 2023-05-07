using BCrypt.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User, Guid> _userRepository;
        protected readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IBaseRepository<User, Guid> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> Login(UserModel userModel)
        {
            var userEntity = await _userRepository.QueryFirst(u => u.Email == userModel.Email);

            if (userEntity == null || !(await PasswordVerify(userModel.Password, userEntity.PasswordHash)))
                throw new ArgumentException("Username or password is incorrect");

            string token = CreateToken(userModel);

            return token;
        }

        public async Task<UserModel> Register(UserModel model)
        {
            if (await _userRepository.QueryFirst(u=>u.Email == model.Email) != null)
                throw new ArgumentException("Username with email " + model.Email + " already exist");

            var userEntity = _mapper.Map<User>(model);

            userEntity.PasswordHash = await HashPasswordAsync(model.Password);
            userEntity.CreatedAt= DateTime.Now;

            var createdEntity = await _userRepository.AddAsync(userEntity);

            return _mapper.Map<UserModel>(createdEntity);
        }

        public async Task<UserModel> Get(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task Update(UserModel userModel)
        {
            if (await _userRepository.QueryFirst(u => u.Email == userModel.Email) != null)
                throw new ArgumentException("Email '" + userModel.Email + "'  already exist");

            var userEntity = _mapper.Map<User>(userModel);

            if (!string.IsNullOrEmpty(userModel.Password))
                userEntity.PasswordHash = await HashPasswordAsync(userModel.Password);

            await _userRepository.UpdateAsync(userEntity);
        }

        public async Task Delete(Guid id)
        {
            var userEntity = await _userRepository.GetAsync(id);

            if (userEntity == null)
                throw new ArgumentException($"User with the id '{id}' could not be found.");

            await _userRepository.DeleteAsync(userEntity);
        }

        private string CreateToken(UserModel userModel)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userModel.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

        private async Task<string> HashPasswordAsync(string password)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));
        }

        private async Task<bool> PasswordVerify(string password, string passwordHash)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.Verify(password, passwordHash));
        }
    }
}
