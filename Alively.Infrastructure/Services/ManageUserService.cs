using Alively.Core.Entities;
using Alively.Core.Repositories;
using Alively.Core.Services;
using Ardalis.GuardClauses;
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
            return await _userRepository.CreateAsync(user, token).ConfigureAwait(false);
        }

        public async Task<bool> DeleteUserAsync(Guid userUuid, CancellationToken token = default)
        {
            try
            {
                await _userRepository.DeleteAsync(userUuid, token).ConfigureAwait(false);

                return true;
            }
            catch
            {
                return false;

            }
        }

        public async Task<User> GetUserAsync(Guid userUuid, CancellationToken token = default)
        {
            return await _userRepository.GetAsync(userUuid, token).ConfigureAwait(false);
        }

        public async Task<User> UpdateUserAsync(User user, CancellationToken token = default)
        {
            Guard.Against.Null(user, nameof(user));

            return await _userRepository.UpdateAsync(user, token).ConfigureAwait(false);
        }
    }
}
