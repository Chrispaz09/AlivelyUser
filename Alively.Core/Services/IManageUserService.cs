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
        
        Task<User> GetUserAsync(int userId, CancellationToken token = default);

        Task<User> UpdateUserAsync(User user, CancellationToken token = default);

        Task<bool> DeleteUserAsync(int userId, CancellationToken token = default);
    }
}
