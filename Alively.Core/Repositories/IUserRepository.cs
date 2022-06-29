using Alively.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user, CancellationToken token = default);

        Task DeleteAsync(Guid uuid, CancellationToken token = default);

        Task<User> GetAsync(Guid uuid, CancellationToken token = default);

        Task<User> UpdateAsync(User user, CancellationToken token = default);

        Task<bool> DoesUsernameAlreadyExist(string username, CancellationToken token = default);

        Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken token = default);

        Task<Guid> GetUserUuidByUsername(string username, CancellationToken token = default);

        Task ChangeUserPassword(string newPassword, Guid uuid, CancellationToken token = default);

        Task<bool> DoesUuidExist(Guid uuid, CancellationToken token = default);
    }
}
