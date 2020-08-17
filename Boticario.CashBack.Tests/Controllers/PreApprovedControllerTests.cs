// ----------------------------------------
// <copyright file=PreApprovedControllerTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories;
using Boticario.CashBack.Services;
using Boticario.CashBack.Tests.Extensions;
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
    public class PreApprovedControllerTests
    {
        #region [ Fields ]

        Mock<ILogger<PreApprovedController>> logger;
        private Mock<IRepository<PreApproved>> preApprovedRepository;
        private Mock<IUserService> userService;
        private PreApprovedController preApprovedController;
        private List<PreApproved> list;
        private PreApproved pre;
        private User user;

        #endregion [ Fields ]

        #region [ Arrange ]

        [SetUp]
        public void Init()
        {
            logger = new Mock<ILogger<PreApprovedController>>();
            preApprovedRepository = new Mock<IRepository<PreApproved>>();
            userService = new Mock<IUserService>();
            preApprovedController = new PreApprovedController(logger.Object);

            list = new List<PreApproved>();
            user = new User();
            pre = new PreApproved();
            pre.SetAllProperties();
            pre.GetPropertyValues();
            pre.ToString();
        }

        #endregion [ Arrange ]

        #region [ Tests ]


        [Test()]
        public async Task CreateAsyncTest()
        {
            pre.Cpf = "123";
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            userService.Setup(a => a.FindUser(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(user);
            var result = await preApprovedController.CreateAsync(preApprovedRepository.Object, userService.Object, pre);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);

        }

        [Test()]
        public async Task CreateAsyncTestCpfNotFound()
        {
            pre.Cpf = "123";
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            var result = await preApprovedController.CreateAsync(preApprovedRepository.Object, userService.Object, pre);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);

        }

        [Test()]
        public async Task CreateAsyncTestCpfInvalid()
        {
            pre.Cpf = "asd";
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            var result = await preApprovedController.CreateAsync(preApprovedRepository.Object, userService.Object, pre);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAsyncTestEx()
        {
            pre.Cpf = "123";
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await preApprovedController.CreateAsync(preApprovedRepository.Object, userService.Object, pre);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTest()
        {
            preApprovedRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(list);
            var result = await preApprovedController.GetAllAsync(preApprovedRepository.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTestEx()
        {

            preApprovedRepository.Setup(a => a.GetAllAsync()).Throws<Exception>();
            var result = await preApprovedController.CreateAsync(preApprovedRepository.Object, userService.Object, pre);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        } 

        #endregion [ Tests  ]

    }
}