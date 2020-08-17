// ----------------------------------------
// <copyright file=UserServiceTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Api.Extensions.SecurityRoles;
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class UserServiceTests
    {
        #region [ Fields ]

        private Mock<IUserRepository> userRepository;
        private IUserService userService;
        private User user;

        #endregion [ Fields ]

        #region [ Arrange ]

        [SetUp]
        public void Init()
        {
            userRepository = new Mock<IUserRepository>();
            userService = new UserService(userRepository.Object);

            user = new User()
            {
                Email = "test@test.com",
                Name = "Jon doe",
                Cpf = "12345",
                Password = "test",
                Role = Roles.SolutionAdministrator
            };
        }

        #endregion [ Arrange ]


        #region [ Tests ]

        [Test()]
        public async Task AuthenticateTest()
        {
            user.Password = "test".ToHashPassword();
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var ret = await userService.Authenticate(user.Email, "test");
            userRepository.Verify(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>()));
            Assert.That(ret.user, Is.Not.Null);
            Assert.That(ret.token, Is.Not.Null);
        }

        [Test()]
        public async Task AuthenticateTestUserNull()
        {
            user.Password = "test".ToHashPassword();
            var ret = await userService.Authenticate(user.Email, "test");
            userRepository.Verify(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>()));
            Assert.That(ret.user, Is.Null);
            Assert.That(ret.token, Is.Empty);
        }

        [Test()]
        public async Task AuthenticateTestPasswordInvalid()
        {
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            user.Password = "test123".ToHashPassword();
            var ret = await userService.Authenticate(user.Email, "test");
            userRepository.Verify(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>()));
            Assert.That(ret.user, Is.Null);
            Assert.That(ret.token, Is.Empty);
        }

        [Test()]
        public void ValideteUserDataTestEmailEx()
        {
            user.Email = "";
            Assert.Throws<InvalidOperationException>(() => userService.ValideteUserData(user));
        }

        [Test()]
        public void ValideteUserDataTestNamelEx()
        {
            user.Name = "";
            Assert.Throws<InvalidOperationException>(() => userService.ValideteUserData(user));
        }

        [Test()]
        public void ValideteUserDataTestCpfEx()
        {
            user.Cpf = "";
            Assert.Throws<InvalidOperationException>(() => userService.ValideteUserData(user));
        }

        [Test()]
        public void ValideteUserDataTestPasswordEx()
        {
            user.Password = "";
            Assert.Throws<InvalidOperationException>(() => userService.ValideteUserData(user));
        }

        [Test()]
        public void ValideteUserDataTestRolelEx()
        {
            user.Role = "";
            Assert.Throws<InvalidOperationException>(() => userService.ValideteUserData(user));
        }

        [Test()]
        public async Task CreateUserTest()
        {
            userRepository.Setup(a => a.AddAsync(It.IsAny<User>())).ReturnsAsync(user);
            var a = await userService.CreateUser(user);
            Assert.That(a, Is.Not.Null);
        }

        [Test()]
        public async Task CreateUserTestExists()
        {
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                 .ReturnsAsync(user);
            Assert.ThrowsAsync<InvalidOperationException>(() => userService.CreateUser(user));
        }

        [Test()]
        public async Task DeleteUserTest()
        {
            userRepository.Setup(a => a.DeleteAsync(It.IsAny<User>())).ReturnsAsync(1);
            var a = await userService.DeleteUser(user);
            Assert.That(a, Is.Not.Null);
        }

        [Test()]
        public async Task DeleteUserTestEx()
        {
            userRepository.Setup(a => a.DeleteAsync(It.IsAny<User>())).ReturnsAsync(0);
            Assert.ThrowsAsync<InvalidOperationException>(() => userService.DeleteUser(user));
        }

        [Test()]
        public async Task GetAllTest()
        {
            userRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<User>() { user });
            var list = await userService.GetAll();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test()]
        public async Task GetUserTest()
        {
            userRepository.Setup(a => a.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            var u = await userService.GetUser(Guid.NewGuid());
            Assert.That(u, Is.Not.Null);
        }

        [Test()]
        public async Task GetUserTestEx()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => userService.GetUser(Guid.NewGuid()));
        }

        [Test()]
        public async Task GetUserByEmailTest()
        {
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var u = await userService.GetUserByEmail("test@test.com");
            Assert.That(u, Is.Not.Null);
        }

        [Test()]
        public void GetUserByEmailTestNull()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => userService.GetUserByEmail(null));
        }

        [Test()]
        public async Task UpdateUserTest()
        {
            userRepository.Setup(a => a.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            var u = await userService.UpdateUser(user);
            Assert.That(u, Is.Not.Null);
        }

        [Test()]
        public async Task UpdateUserTestEx()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => userService.UpdateUser(user));
        }

        [Test()]
        public async Task FindUserTest()
        {
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                 .ReturnsAsync(user);
            var ret = await userService.FindUser(a => a.Cpf == "2314");
            Assert.That(ret, Is.Not.Null);
        } 

        #endregion [ Tests ]
    }
}