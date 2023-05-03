using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    internal class UserService : IUserService
    {
        private readonly IBaseRepository<User, int> _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IBaseRepository<User, int> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> Add(UserModel address)
        {
            var entity = _mapper.Map<User>(address);
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

        public async Task Delete(Guid id)
        {
            var userEntity = await _userRepository.GetAsync(id);

            if (userEntity == null)
                throw new ArgumentException($"User with the id '{id}' could not be found.");

            await _userRepository.DeleteAsync(userEntity);
        }
    }
}
