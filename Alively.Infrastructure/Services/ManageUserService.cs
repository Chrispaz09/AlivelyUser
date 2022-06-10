using Alively.Core.Entities;
using Alively.Core.Repositories;
using Alively.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Infrastructure.Services
{
    public class ManageUserService : IManageUserService
    {
        public readonly IUserRepository _userRepository;

        public ManageUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken token = default)
        {
            try
            {
                return await _userRepository.CreateAsync(user, token).ConfigureAwait(false);
            }
            catch 
            {

                return User.NotValid;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId, CancellationToken token = default)
        {
            try
            {
                await _userRepository.DeleteAsync(userId, token).ConfigureAwait(false);

                return true;
            }
            catch 
            {
                return false;
                
            }
        }

        public async Task<User> GetUserAsync(int userId, CancellationToken token = default)
        {
            try
            {
                return await _userRepository.GetAsync(userId, token).ConfigureAwait(false);
            }
            catch 
            {
                return User.NotFound;
            }
        }

        public async Task<User> UpdateUserAsync(User user, CancellationToken token = default)
        {
            try
            {
                return await _userRepository.UpdateAsync(user, token).ConfigureAwait(false);
            }
            catch 
            {
                return User.NotValid;
            }
        }
    }
}
