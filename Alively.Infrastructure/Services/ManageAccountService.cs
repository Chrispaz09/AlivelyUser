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
            Guard.Against.NullOrWhiteSpace(username, nameof(username), "username is missing. ");

            return await _userRepository.DoesUsernameAlreadyExist(username, token);
        }

        public Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(email, nameof(email), "email is missing. ");

            return _userRepository.IsEmailAlreadyRegistered(email, token);
        }

        public string SecurePassword(string password, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password), "Password is missing. ");

            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public async Task<bool> VerifyHashPassword(Guid userUuid, string password, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(password, nameof(password), "Password is missing. ");

            var user = await _userRepository.GetAsync(userUuid, token).ConfigureAwait(false);

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public async Task<Guid> GetUserUuidByUsername(string username, CancellationToken token = default)
        {
            Guard.Against.Null(username, nameof(username), "username is missing. ");

            return await _userRepository.GetUserUuidByUsername(username, token).ConfigureAwait(false);
        }

        public async Task ChangePassword(string newPassword, Guid userUuid, CancellationToken token = default)
        {
            Guard.Against.NullOrEmpty(newPassword, nameof(newPassword));

            newPassword = SecurePassword(newPassword, token);

            await _userRepository.ChangeUserPassword(newPassword, userUuid ,token).ConfigureAwait(false);
        }

        public async Task<bool> DoesUuidExist(Guid uuid, CancellationToken token = default)
        {
            return await _userRepository.DoesUuidExist(uuid, token).ConfigureAwait(false);
        }
    }
}
