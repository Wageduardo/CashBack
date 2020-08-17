// ----------------------------------------
// <copyright file=PurchaseControllerTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
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
    public class PurchaseControllerTests
    {
        #region [ Fields ]

        private Mock<IPurchaseService> purchaseService;
        private Mock<IUserService> userService;
        private Mock<ILogger<PurchaseController>> logger;
        private PurchaseController purchaseController;

        private List<Purchase> list;
        private Purchase purchase;
        private User user;

        #endregion [ Fields ]

        #region [ Arrange ]


        [SetUp]
        public void Init()
        {
            userService = new Mock<IUserService>();
            purchaseService = new Mock<IPurchaseService>();

            logger = new Mock<ILogger<PurchaseController>>();
            purchaseController = new PurchaseController(logger.Object);

            list = new List<Purchase>();
            purchase = new Purchase();
            purchase.SetAllProperties();
            purchase.GetPropertyValues();
            purchase.ToString();
            user = new User();
            user.SetAllProperties();
            user.GetPropertyValues();
            user.ToString();

        }

        #endregion [ Arrange  ]

        #region [ Tests ]

        // get me
        [Test()]
        public async Task GetAsyncTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            purchaseService.Setup(a => a.GetUserPurchases(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(list);
            var result = await purchaseController.GetAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAsyncTestNotFoundResult()
        {
            var result = await purchaseController.GetAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAsyncTestException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await purchaseController.GetAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAsyncTestInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await purchaseController.GetAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }


        //GetAllAsync
        // get me
        [Test()]
        public async Task GetAllAsyncTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            purchaseService.Setup(a => a.GetUserPurchases(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(list);
            var result = await purchaseController.GetAllAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTestNotFoundResult()
        {
            var result = await purchaseController.GetAllAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTestException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await purchaseController.GetAllAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetAllAsyncTestInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await purchaseController.GetAllAsync(purchaseService.Object, userService.Object);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        //Get id
        [Test()]
        public async Task GetIdAsyncTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            purchaseService.Setup(a => a.GetPurchase(It.IsAny<Guid>())).ReturnsAsync(purchase);
            var result = await purchaseController.GetIdAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetIdAsyncTestNotFound()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            var result = await purchaseController.GetIdAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetIdAsyncTestNotFoundResult()
        {
            var result = await purchaseController.GetIdAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetIdAsyncTestException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await purchaseController.GetIdAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetIdAsyncTestInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await purchaseController.GetIdAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }


        // CreateAsync
        [Test()]
        public async Task CreateAsyncTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            purchaseService.Setup(a => a.CreatePurchase(It.IsAny<Purchase>(), It.IsAny<User>())).ReturnsAsync(purchase);
            var result = await purchaseController.CreateAsync(purchaseService.Object, userService.Object, purchase);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAsyncTestUserNorFound()
        {
            var result = await purchaseController.CreateAsync(purchaseService.Object, userService.Object, purchase);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAsyncTestException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await purchaseController.CreateAsync(purchaseService.Object, userService.Object, purchase);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task CreateAsyncTestUserInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await purchaseController.CreateAsync(purchaseService.Object, userService.Object, purchase);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }


        // UpdateAsync
        [Test()]
        public async Task UpdateAsyncTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            purchaseService.Setup(a => a.UpdatePurchase(It.IsAny<Purchase>(), It.IsAny<User>())).ReturnsAsync(purchase);
            var result = await purchaseController.UpdateAsync(purchaseService.Object, userService.Object, Guid.NewGuid(), purchase);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task UpdateAsyncTestNotFound()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            var result = await purchaseController.UpdateAsync(purchaseService.Object, userService.Object, Guid.NewGuid(), purchase);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task UpdateAsyncTestUserNorFound()
        {
            var result = await purchaseController.UpdateAsync(purchaseService.Object, userService.Object, Guid.NewGuid(), purchase);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task UpdateAsyncTestException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await purchaseController.UpdateAsync(purchaseService.Object, userService.Object, Guid.NewGuid(), purchase);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task UpdateAsyncTestUserInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await purchaseController.UpdateAsync(purchaseService.Object, userService.Object, Guid.NewGuid(), purchase);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        // DeleteAsync
        [Test()]
        public async Task DeleteAsyncTest()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            purchaseService.Setup(a => a.GetPurchase(It.IsAny<Guid>())).ReturnsAsync(purchase);
            purchaseService.Setup(a => a.DeletePurchase(It.IsAny<Purchase>())).ReturnsAsync(1);
            var result = await purchaseController.DeleteAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task DeleteAsyncTestNotFound()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).ReturnsAsync(user);
            var result = await purchaseController.DeleteAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task DeleteAsyncTestUserNorFound()
        {
            var result = await purchaseController.DeleteAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
            var ok = result as NotFoundObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task DeleteAsyncException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<Exception>();
            var result = await purchaseController.DeleteAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task DeleteAsyncTestUserInvalidOperationException()
        {
            userService.Setup(a => a.GetUserByEmail(It.IsAny<string>(), true)).Throws<InvalidOperationException>();
            var result = await purchaseController.DeleteAsync(purchaseService.Object, userService.Object, Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ok = result as BadRequestObjectResult;
            Assert.That(ok, Is.Not.Null);
        } 

        #endregion [ MyRegion ]
    }
}