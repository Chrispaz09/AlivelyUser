using Alively.Core.Entities;
using Alively.Core.Repositories;
using Alively.Infrastructure.Data;
using Alively.Infrastructure.Validators;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AlivelyDbContext _context;

        public UserRepository(AlivelyDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user, CancellationToken token = default)
        {
            Guard.Against.Null(user, nameof(user), "User is missing. ");

            user.Uuid = Guid.NewGuid();

            UserValidator.Validate(user);

            var addedUser = await _context.Users.AddAsync(user, token).ConfigureAwait(false);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);

            return addedUser.Entity;
        }

        public async Task DeleteAsync(Guid uuid, CancellationToken token = default)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(users => users.Uuid == uuid, token).ConfigureAwait(false);

            Guard.Against.Null(getUser, nameof(getUser));

            _context.Users.Remove(getUser);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<bool> DoesUsernameAlreadyExist(string username, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));

            return await _context.Users.AnyAsync(users => users.UserName == username).ConfigureAwait(false);
        }

        public async Task<User> GetAsync(Guid uuid, CancellationToken token = default)
        {
            return await _context.Users.FirstOrDefaultAsync(users => users.Uuid == uuid, token).ConfigureAwait(false);
        }

        public async Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(email, nameof(email));

            return await _context.Users.AnyAsync(users => users.Email == email, token).ConfigureAwait(false);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken token = default)
        {
            Guard.Against.Null(user, nameof(user));

            var userToUpdate = await GetAsync(user.Uuid, token).ConfigureAwait(false);

            Guard.Against.Null(userToUpdate, nameof(userToUpdate), "User was not found.");

            userToUpdate.FirstName = user.FirstName;

            userToUpdate.LastName = user.LastName;

            userToUpdate.Email = user.Email;

            userToUpdate.Age = user.Age;

            userToUpdate.UserName = user.UserName;

            await _context.SaveChangesAsync(token);

            return await GetAsync(user.Uuid).ConfigureAwait(false);
        }
        
        public async Task<Guid> GetUserUuidByUsername(string username, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));

            var getUser = await _context.Users.FirstOrDefaultAsync(users => users.UserName == username).ConfigureAwait(false);

            if(getUser is null)
            {
                return Guid.Empty;
            }

            return getUser.Uuid;
        }

        public async Task ChangeUserPassword(string newPassword, Guid uuid, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(newPassword, nameof(newPassword));

            var user = await _context.Users.FirstOrDefaultAsync(users => users.Uuid == uuid, token).ConfigureAwait(false);

            Guard.Against.Null(user);

            user.Password = newPassword;

            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<bool> DoesUuidExist(Guid uuid, CancellationToken token = default)
        {
            return await _context.Users.AnyAsync(users => users.Uuid == uuid).ConfigureAwait(false);
        }
    }
}
