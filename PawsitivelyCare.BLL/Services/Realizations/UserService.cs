using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User, int> _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IBaseRepository<User, int> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> Add(UserModel user)
        {
            var entity = _mapper.Map<User>(user);
            var createdEntity = await _userRepository.AddAsync(entity);

            return _mapper.Map<UserModel>(createdEntity);
        }

        public async Task<UserModel> Get(int id)
        {
            var User = await _userRepository.GetAsync(id);

            return _mapper.Map<UserModel>(User);
        }

        public async Task Update(UserModel userModel)
        {
            var userEntity = _mapper.Map<User>(userModel);

            await _userRepository.UpdateAsync(userEntity);
        }

        public async Task Delete(int id)
        {
            var userEntity = await _userRepository.GetAsync(id);

            if (userEntity == null)
                throw new ArgumentException($"User with the id '{id}' could not be found.");

            await _userRepository.DeleteAsync(userEntity);
        }
    }
}
