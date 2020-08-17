// ----------------------------------------
// <copyright file=AuthControllerTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.ViewModel;
using Boticario.CashBack.Services;
using Boticario.CashBack.Tests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Boticario.CashBack.Api.Controllers.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class AuthControllerTests
    {
        #region [ Fields ]

        private Mock<IUserService> userService;
        private Mock<ILogger<AuthController>> logger;
        private AuthController authController;

        #endregion [ Fields ]

        #region [ Arrange ]

        [SetUp]
        public void Init()
        {
            userService = new Mock<IUserService>();
            logger = new Mock<ILogger<AuthController>>();
            authController = new AuthController();
            var a = new AuthModel();
            a.SetAllProperties();
            a.GetPropertyValues();
            a.ToString();
        }

        #endregion [ Arrange ]

        #region [ Tests ]

        [Test()]
        public async Task AuthTest()
        {
            userService.Setup(a => a.Authenticate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((new User(), "token"));
            var result = await authController.Login(logger.Object, userService.Object, new AuthModel());
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task AuthTestFail()
        {
            var result = await authController.Login(logger.Object, userService.Object, new AuthModel());
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
            var ret = result as UnauthorizedObjectResult;
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task AuthTestFailEx()
        {
            userService.Setup(a => a.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
            var result = await authController.Login(logger.Object, userService.Object, new AuthModel());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        } 

        #endregion [ Tests  ]
    }
}