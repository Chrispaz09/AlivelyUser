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
            Guard.Against.Null(user, nameof(user));

            UserValidator.Validate(user);

            var addedUser = await _context.Users.AddAsync(user, token).ConfigureAwait(false);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);

            return addedUser.Entity;
        }

        public async Task DeleteAsync(int id, CancellationToken token = default)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            var getUser = await _context.Users.FirstOrDefaultAsync(users => users.Id == id, token).ConfigureAwait(false);

            Guard.Against.Null(getUser, nameof(getUser));

            _context.Users.Remove(getUser);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<bool> DoesUsernameAlreadyExist(string username, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));

            return await _context.Users.AnyAsync(users => users.UserName == username).ConfigureAwait(false);
        }

        public async Task<User> GetAsync(int id, CancellationToken token = default)
        {
            Guard.Against.NegativeOrZero(id, nameof(id));

            return await _context.Users.FirstOrDefaultAsync(users => users.Id == id, token).ConfigureAwait(false);
        }

        public async Task<bool> IsEmailAlreadyRegistered(string email, CancellationToken token = default)
        {
            Guard.Against.NullOrWhiteSpace(email, nameof(email));

            return await _context.Users.AnyAsync(users => users.Email == email, token).ConfigureAwait(false);
        }

        public async Task<User> UpdateAsync(User user, CancellationToken token = default)
        {
            Guard.Against.Null(user, nameof(user));

            UserValidator.Validate(user);

            var addedUser = _context.Users.Update(user);

            await _context.SaveChangesAsync(token);

            return addedUser.Entity;
        }
    }
}
