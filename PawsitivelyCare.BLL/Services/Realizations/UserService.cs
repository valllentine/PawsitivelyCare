using BCrypt.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User, Guid> _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IBaseRepository<User, Guid> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> Register(UserModel model)
        {
            if (_userRepository.QueryFirst(u=>u.Email == model.Email) != null)
                throw new ArgumentException("Username with email " + model.Email + " already exist");

            var userEntity = _mapper.Map<User>(model);

            userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            userEntity.CreatedAt= DateTime.Now;

            var createdEntity = await _userRepository.AddAsync(userEntity);

            return _mapper.Map<UserModel>(createdEntity);
        }

        public async Task<UserModel> Get(Guid id)
        {
            var User = await _userRepository.GetAsync(id);

            return _mapper.Map<UserModel>(User);
        }

        public async Task Update(UserModel userModel)
        {
            var userEntity = _mapper.Map<User>(userModel);

            await _userRepository.UpdateAsync(userEntity);
        }

        public async Task Delete(Guid id)
        {
            var userEntity = await _userRepository.GetAsync(id);

            if (userEntity == null)
                throw new ArgumentException($"User with the id '{id}' could not be found.");

            await _userRepository.DeleteAsync(userEntity);
        }
    }
}
