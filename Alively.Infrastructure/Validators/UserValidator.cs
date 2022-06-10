using Alively.Core.Entities;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Infrastructure.Validators
{
    public static class UserValidator
    {
        public static void Validate(User user)
        {
            Guard.Against.Null(user, nameof(user), "User is missing. ");

            if (user.Uuid == Guid.Empty)
            {
                throw new ArgumentNullException("userUuid is empty. ");
            };

            Guard.Against.NullOrWhiteSpace(user.Email, nameof(user.Email), "Email is missing. ");

            Guard.Against.NullOrWhiteSpace(user.Password, nameof(user), "Password is missing. ");

            Guard.Against.NullOrWhiteSpace(user.UserName, nameof(user.UserName), "UserName is missing. ");

            Guard.Against.NullOrWhiteSpace(user.FirstName, nameof(user.FirstName), "Firstname is missing. ");

            Guard.Against.NullOrWhiteSpace(user.LastName, nameof(user.LastName), "LastName is missing. ");

            Guard.Against.NullOrWhiteSpace(user.Password, nameof(user), "Password is missing. ");
        }
    }
}
