using Alively.Core.Entities;
using Alively.Core.Repositories;
using Alively.Infrastructure.Data;
using Alively.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.InfrastructureTest.Repositories
{
    internal class UserRepositoryTest
    {
        public IUserRepository _userRepository;

        public AlivelyDbContext _context;

        public User _userTest;

        [SetUp]
        public void Setup()
        {
            _userTest = new()
            {
                FirstName = "test",
                LastName = "tester",
                Email = "test@validemail.com",
                Password = "validPassword",
                Age = 30,
                UserName = "testValidUsername"
            };

            _context = new AlivelyDbContext();

            _userRepository = new UserRepository(_context);
        }

        /** Start Unit Testing **/
        #region CreateUser
        [Test]
        public async Task CreateUser_UserIsValid_ReturnsUserAdded()
        {
            var addedUser = await _userRepository.CreateAsync(_userTest).ConfigureAwait(false);

            addedUser.Uuid.ShouldBe(_userTest.Uuid);
        }
        #endregion


        #region GetUser
        [Test]
        public async Task GetUser_UserIdMatchesUserInDatabase_ReturnsUserAssociatedToId()
        {
            var addedUser = await _userRepository.CreateAsync(_userTest).ConfigureAwait(false);

            var getUser = await _userRepository.GetAsync(addedUser.Id).ConfigureAwait(false);

            getUser.Uuid.ShouldBe(_userTest.Uuid);
        }
        #endregion

        #region UpdateUser
        [Test]
        public async Task UpdateUser_UserIsUpdatedInDatabase_ReturnsUserWithChanges()
        {
            var addedUser = await _userRepository.CreateAsync(_userTest).ConfigureAwait(false);

            var newGuid = Guid.NewGuid();

            addedUser.Uuid = newGuid;

            var updateUser = await _userRepository.UpdateAsync(addedUser).ConfigureAwait(false);

            updateUser.Uuid.ShouldBe(addedUser.Uuid);
        }
        #endregion

        #region DeleteUser
        [Test]
        public async Task DeleteUser_UserIsRemoved_GetReturnsNull()
        {
            var addedUser = await _userRepository.CreateAsync(_userTest).ConfigureAwait(false);

            await _userRepository.DeleteAsync(addedUser.Id).ConfigureAwait(false);

            var getUser = await _userRepository.GetAsync(addedUser.Id).ConfigureAwait(false);

            getUser.ShouldBeNull();
        }
        #endregion

        /**End Unit Testing **/
    }
}
