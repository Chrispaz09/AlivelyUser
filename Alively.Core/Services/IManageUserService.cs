using Alively.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Core.Services
{
    public interface IManageUserService
    {
        Task<User> CreateUserAsync(User user, CancellationToken token = default);
        
        Task<User> GetUserAsync(Guid userUuid, CancellationToken token = default);

        Task<User> UpdateUserAsync(User user, CancellationToken token = default);

        Task<bool> DeleteUserAsync(Guid userUuid, CancellationToken token = default);
    }
}
