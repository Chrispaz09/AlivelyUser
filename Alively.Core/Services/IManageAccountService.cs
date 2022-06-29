using Alively.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Core.Services
{
    public interface IManageAccountService
    {
        /// <summary>
        /// Transforms the password.
        /// </summary>
        /// <param name="password">The password to secure. </param>
        /// <param name="token">The Cancellation token. </param>
        /// <returns></returns>
        string SecurePassword(string password, CancellationToken token = default);

        /// <summary>
        /// Verifies that the hashed password matches hashed password in the database.
        /// </summary>
        /// <param name="password">The hashed password. </param>
        /// <param name="token">The Cancellation token. </param>
        /// <returns>True if the hashed password matches the hash password in the database. </returns>
        Task<bool> VerifyHashPassword(Guid userUuid, string password, CancellationToken token = default);

        Task<bool> DoesUsernameAlreadyExist(string username, CancellationToken token = default);

        Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken token = default);

        Task<Guid> GetUserUuidByUsername(string username, CancellationToken token = default);

        Task ChangePassword(string newPassword, Guid userUuid, CancellationToken token = default);

        Task<bool> DoesUuidExist(Guid uuid, CancellationToken token = default);
    }
}
