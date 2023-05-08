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
using Microsoft.Extensions.Options;
using PawsitivelyCare.BLL.Common.Auth;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User, Guid> _userRepository;
        protected readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IOptions<AuthOptions> _authOptions;

        public UserService(IBaseRepository<User, Guid> userRepository, IMapper mapper, IConfiguration configuration, IOptions<AuthOptions> authOptions)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _authOptions = authOptions;
        }

        public async Task<UserModel> AuthenticateUser(UserModel userModel)
        {
            var userEntity = await _userRepository.QueryFirst(u => u.Email == userModel.Email);

            if (!(await PasswordVerify(userModel.Password, userEntity.PasswordHash)))
                userEntity = null;

            return _mapper.Map<UserModel>(userEntity);
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

        public async Task<UserModel> GetByEmail(string email)
        {
            return _mapper.Map<UserModel>(await _userRepository.QueryFirst(u => u.Email == email));
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

        public async Task<string> GenerateJwtToken(UserModel userModel)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString())
            };

            var token = new JwtSecurityToken(
                authParams.Issuer, 
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddDays(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
