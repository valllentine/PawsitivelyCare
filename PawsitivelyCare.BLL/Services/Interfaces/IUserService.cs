using PawsitivelyCare.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    internal interface IUserService
    {
        Task<UserModel> Add(UserModel user);
        Task<UserModel> Get(int id);
        Task Update(UserModel user);
        Task Delete(Guid id);
    }
}
