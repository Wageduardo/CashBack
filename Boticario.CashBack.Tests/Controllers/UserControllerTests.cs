// ----------------------------------------
// <copyright file=UserControllerTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core.SecurityRoles;
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.ViewModel;
using Boticario.CashBack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.CashBack.Api.Controllers.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class UserControllerTests
    {
        #region [ Fields ]

        private Mock<ILogger<UserController>> logger;
        private Mock<IUserService> userService;
        private Mock<IExternalService> externalService;
        private Mock<IPurchaseService> purchaseService;

        private List<User> list;


        private UserController userController;

        #endregion [ Fields ]
        
        #region [ Arrange ]


        [SetUp]
        public void Init()
        {
            userService = new Mock<IUserService>();
            externalService = new Mock<IExternalService>();
            purchaseService = new Mock<IPurchaseService>();

            logger = new Mock<ILogger<UserController>>();
            userController = new UserController(logger.Object);

            list = new List<User>();
        }

        #endregion [ Arrange ]

        #region [ Tests ]

        // GetAll
        [Test()]
        public async Task GetAllAsyncTest()
        {
            list.Add(new User());
            userService.Setup(a => a.GetAll()).ReturnsAsync(list);
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.GetAllAsync(userService.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTestEmpty()
        {
            userService.Setup(a => a.GetAll()).ReturnsAsync(list);
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.GetAllAsync(userService.Object);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }


        [Test()]
        public async Task GetAllAsyncTestExeption()
        {
            list.Add(new User());
            userService.Setup(a => a.GetAll()).ReturnsAsync(list);
            var result = await userController.GetAllAsync(userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTestInvalidOperationException()
        {
            list.Add(new User());
            userService.Setup(a => a.GetAll()).ReturnsAsync(list);
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>(); ;
            var result = await userController.GetAllAsync(userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }


        /// Get
        [Test()]
        public async Task GetTest()
        {
            userService.Setup(a => a.GetUser(It.IsAny<Guid>())).ReturnsAsync(new User());
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.Get(userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetTestEmpty()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.Get(userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }


        [Test()]
        public async Task GetTestExeption()
        {
            userService.Setup(a => a.GetUser(It.IsAny<Guid>())).ReturnsAsync(new User());
            var result = await userController.Get(userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetTestInvalidOperationException()
        {
            userService.Setup(a => a.GetUser(It.IsAny<Guid>())).ReturnsAsync(new User());
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>(); ;
            var result = await userController.Get(userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }


        //Me
        /// Get
        [Test()]
        public async Task GetMeTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.GetMe(userService.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetMeTestEmpty()
        {
            var result = await userController.GetMe(userService.Object);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }


        [Test()]
        public async Task GetMeTestExeption()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await userController.GetMe(userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetMeTestInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await userController.GetMe(userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        //create
        [Test()]
        public async Task CreateTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            userService.Setup(a => a.CreateUser(It.IsAny<User>())).ReturnsAsync(new User());
            var result = await userController.Create(userService.Object, new User());
            Assert.That(result, Is.InstanceOf<CreatedResult>());
            var ok = result as CreatedResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task CreateTestException()
        {
            userService.Setup(a => a.CreateUser(It.IsAny<User>())).Throws<Exception>();
            var result = await userController.Create(userService.Object, new User());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task CreateTestInvalidOperationException()
        {

            userService.Setup(a => a.CreateUser(It.IsAny<User>())).Throws<InvalidOperationException>();
            var result = await userController.Create(userService.Object, new User());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        //create admin
        [Test()]
        public async Task CreateAdmintUserTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            userService.Setup(a => a.CreateUser(It.IsAny<User>())).ReturnsAsync(new User());
            var result = await userController.CreateAdmintUser(userService.Object, new User() { Role = Policies.Administrator });
            Assert.That(result, Is.InstanceOf<CreatedResult>());
            var ok = result as CreatedResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAdmintUserException()
        {
            userService.Setup(a => a.CreateUser(It.IsAny<User>())).Throws<Exception>();
            var result = await userController.CreateAdmintUser(userService.Object, new User() { Role = Policies.Administrator });
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAdmintUserInvalidOperationException()
        {

            userService.Setup(a => a.CreateUser(It.IsAny<User>())).Throws<InvalidOperationException>();
            var result = await userController.CreateAdmintUser(userService.Object, new User() { Role = Policies.Administrator });
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAdmintUserInvalidRole()
        {
            var result = await userController.CreateAdmintUser(userService.Object, new User());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        // GetMyExternalAccumulatedCashback
        [Test()]
        public async Task GetMyExternalAccumulatedCashbackTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            externalService.Setup(a => a.GetCashback(It.IsAny<string>())).ReturnsAsync(new ExternalViewModel() { StatusCode = 200, Body = new Body() { Credit = 500 } });
            var result = await userController.GetMyExternalAccumulatedCashback(userService.Object, externalService.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetMyExternalAccumulatedCashbackTestEx()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await userController.GetMyExternalAccumulatedCashback(userService.Object, externalService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetMyExternalAccumulatedCashbackTestNotFoudn()
        {
            var result = await userController.GetMyExternalAccumulatedCashback(userService.Object, externalService.Object);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ret = result as NotFoundObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        // GetExternalAccumulatedCashback
        [Test()]
        public async Task GetExternalAccumulatedCashbackCpfTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            externalService.Setup(a => a.GetCashback(It.IsAny<string>())).ReturnsAsync(new ExternalViewModel() { StatusCode = 200, Body = new Body() { Credit = 500 } });
            var result = await userController.GetExternalAccumulatedCashback(userService.Object, externalService.Object, "123");
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetExternalAccumulatedCashbackCpfTestEx()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await userController.GetExternalAccumulatedCashback(userService.Object, externalService.Object, "123");
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetExternalAccumulatedCashbackCpfInvalidTestEx()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.GetExternalAccumulatedCashback(userService.Object, externalService.Object, "asd");
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        //GetMyInternalAccumulatedCashback
        [Test()]
        public async Task GetMyInternalAccumulatedCashbackTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            purchaseService.Setup(a => a.GetCashback(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(500);
            var result = await userController.GetMyInternalAccumulatedCashback(userService.Object, purchaseService.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetMyInternalAccumulatedCashbackTestEx()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await userController.GetMyInternalAccumulatedCashback(userService.Object, purchaseService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }


        [Test()]
        public async Task GetMyInternalAccumulatedCashbackTestNotFound()
        {

            var result = await userController.GetMyInternalAccumulatedCashback(userService.Object, purchaseService.Object);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ret = result as NotFoundObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        // GetInternalAccumulatedCashback
        [Test()]
        public async Task GetInternalAccumulatedCashbackCpfTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            purchaseService.Setup(a => a.GetCashback(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(500);
            var result = await userController.GetInternalAccumulatedCashback(userService.Object, purchaseService.Object, "123");
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetInternalAccumulatedCashbackCpfTestEx()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await userController.GetInternalAccumulatedCashback(userService.Object, purchaseService.Object, "123");
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetInternalAccumulatedCashbackInvalidTestEx()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(new User());
            var result = await userController.GetInternalAccumulatedCashback(userService.Object, purchaseService.Object, "asd");
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        } 

        #endregion [ MyRegion ]
    }
}