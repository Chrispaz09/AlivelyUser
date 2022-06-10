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
    public class ManageAccountService : IManageAccountService
    {
        public readonly IUserRepository _userRepository;

        public ManageAccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DoesUsernameAlreadyExist(string username, CancellationToken token = default)
        {
            return await _userRepository.DoesUsernameAlreadyExist(username, token);
        }

        public Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken token = default)
        {
            return _userRepository.IsEmailAlreadyRegistered(email, token);
        }

        public string SecurePassword(string password, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password), "Password is missing. ");

            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public async Task<bool> VerifyHashPassword(int userId, string password, CancellationToken token = default)
        {
            Guard.Against.Null(userId, nameof(userId), "User Id is missing. ");

            Guard.Against.NullOrWhiteSpace(password, nameof(password), "Password is missing. ");

            var user = await _userRepository.GetAsync(userId, token).ConfigureAwait(false);

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
