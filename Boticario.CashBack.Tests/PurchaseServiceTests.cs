// ----------------------------------------
// <copyright file=PurchaseServiceTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.Interfaces.Enums;
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
    public class PurchaseServiceTests
    {
        #region [ Fields ]

        private Mock<IPurchaseRepository> purchaseRepository;
        private Mock<IRepository<PercentageValuePromotion>> percentageValuePromotionRepository;
        private Mock<IRepository<PreApproved>> preApprovedRpository;
        private Mock<IUserRepository> userRepository;
        private IPurchaseService purchaseService;

        private Purchase purchase;
        private User user;

        readonly List<PercentageValuePromotion> listPercentageValuePromotion = new List<PercentageValuePromotion>() {
                                                                               new PercentageValuePromotion() { Value = 0, Percentage = 10},
                                                                               new PercentageValuePromotion() { Value = 1000, Percentage = 15 } };

        #endregion [ Fields ]


        #region [ Arrange ]

        [SetUp()]
        public void Initiliaze()
        {
            purchaseRepository = new Mock<IPurchaseRepository>();
            percentageValuePromotionRepository = new Mock<IRepository<PercentageValuePromotion>>();
            preApprovedRpository = new Mock<IRepository<PreApproved>>();
            userRepository = new Mock<IUserRepository>();
            purchaseService = new PurchaseService(purchaseRepository.Object, percentageValuePromotionRepository.Object, preApprovedRpository.Object, userRepository.Object);

            purchase = new Purchase()
            {
                Cpf = "123",
                Value = 1000,
                Code = "as34sd123",
                Email = "test@test.com"
            };

            user = new User()
            {
                Cpf = purchase.Cpf,
                Email = purchase.Email
            };
        }

        #endregion [ Arrange ]


        #region [ Tests ]

        [Test()]
        public async Task CreatePurchaseTest()
        {
            purchaseRepository.Setup(a => a.AddAsync(It.IsAny<Purchase>())).ReturnsAsync(purchase);
            percentageValuePromotionRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(listPercentageValuePromotion);
            var ret = await purchaseService.CreatePurchase(purchase, user);
            percentageValuePromotionRepository.Verify(a => a.GetAllAsync());
            purchaseRepository.Verify(a => a.AddAsync(It.IsAny<Purchase>()));
            Assert.That(ret, Is.Not.Null);
            Assert.That(ret.CashbackPercent, Is.EqualTo(10.0));
            Assert.That(ret.CashbackValue, Is.EqualTo(100.0));
            Assert.That(ret.Status, Is.EqualTo(ECashbackStatus.OnChecking));

            preApprovedRpository.Setup(a => a.AnyAsync(It.IsAny<Expression<Func<PreApproved, bool>>>())).ReturnsAsync(true);
            ret.Value = 10000;
            ret = await purchaseService.CreatePurchase(purchase, user);
            percentageValuePromotionRepository.Verify(a => a.GetAllAsync());
            purchaseRepository.Verify(a => a.AddAsync(It.IsAny<Purchase>()));
            Assert.That(ret, Is.Not.Null);
            Assert.That(ret.CashbackPercent, Is.EqualTo(15.0));
            Assert.That(ret.CashbackValue, Is.EqualTo(1500.0));
            Assert.That(ret.Status, Is.EqualTo(ECashbackStatus.Approved));

            ret.Value = 0;
            ret = await purchaseService.CreatePurchase(purchase, user);
            percentageValuePromotionRepository.Verify(a => a.GetAllAsync());
            purchaseRepository.Verify(a => a.AddAsync(It.IsAny<Purchase>()));
            Assert.That(ret, Is.Not.Null);
            Assert.That(ret.CashbackPercent, Is.EqualTo(0));
            Assert.That(ret.CashbackValue, Is.EqualTo(0));
            Assert.That(ret.Status, Is.EqualTo(ECashbackStatus.Approved));
        }

        [Test()]
        public void ValidateMandatoryFieldsTestCpfEx()
        {
            purchase.Cpf = "";
            Assert.ThrowsAsync<InvalidOperationException>(() => purchaseService.CreatePurchase(purchase, user));
        }

        [Test()]
        public void ValidateMandatoryFieldsTestEmailEx()
        {
            purchase.Email = "";
            Assert.ThrowsAsync<InvalidOperationException>(() => purchaseService.CreatePurchase(purchase, user));
        }

        [Test()]
        public void ValidateMandatoryFieldsTestCpfDiffEx()
        {
            user.Cpf = "";
            Assert.ThrowsAsync<InvalidOperationException>(() => purchaseService.CreatePurchase(purchase, user));
        }

        [Test()]
        public void ValidateMandatoryFieldsTestEmailDiffEx()
        {
            user.Email = "";
            Assert.ThrowsAsync<InvalidOperationException>(() => purchaseService.CreatePurchase(purchase, user));
        }

        [Test()]
        public async Task UpdatePurchaseTest()
        {
            purchaseRepository.Setup(a => a.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(purchase);
            purchaseRepository.Setup(a => a.UpdateAsync(It.IsAny<Purchase>(), It.IsAny<Guid>())).ReturnsAsync(purchase);
            percentageValuePromotionRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(listPercentageValuePromotion);
            var ret = await purchaseService.UpdatePurchase(purchase, user);
            percentageValuePromotionRepository.Verify(a => a.GetAllAsync());
            purchaseRepository.Verify(a => a.UpdateAsync(It.IsAny<Purchase>(), It.IsAny<Guid>()));
            Assert.That(ret, Is.Not.Null);
            Assert.That(ret.CashbackPercent, Is.EqualTo(10.0));
            Assert.That(ret.CashbackValue, Is.EqualTo(100.0));
            Assert.That(ret.Status, Is.EqualTo(ECashbackStatus.OnChecking));

            preApprovedRpository.Setup(a => a.AnyAsync(It.IsAny<Expression<Func<PreApproved, bool>>>())).ReturnsAsync(true);
            ret.Value = 10000;
            ret.Status = ECashbackStatus.Approved;
            ret = await purchaseService.UpdatePurchase(purchase, user);
            percentageValuePromotionRepository.Verify(a => a.GetAllAsync());
            purchaseRepository.Verify(a => a.UpdateAsync(It.IsAny<Purchase>(), It.IsAny<Guid>()));
            Assert.That(ret, Is.Not.Null);
            Assert.That(ret.CashbackPercent, Is.EqualTo(15.0));
            Assert.That(ret.CashbackValue, Is.EqualTo(1500.0));
            Assert.That(ret.Status, Is.EqualTo(ECashbackStatus.Approved));
        }

        [Test()]
        public async Task UpdatePurchaseTestNull()
        {
            var ret = await purchaseService.UpdatePurchase(purchase, user);
            Assert.That(ret, Is.Null);
        }

        [Test()]
        public async Task GetPurchaseTest()
        {
            purchaseRepository.Setup(a => a.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(purchase);
            var ret = await purchaseService.GetPurchase(Guid.NewGuid());
            Assert.That(ret, Is.Not.Null);
        }

        [Test()]
        public async Task GetPurchasesTest()
        {
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User());
            purchaseRepository.Setup(a => a.FindAllAsync(It.IsAny<Expression<Func<Purchase, bool>>>())).ReturnsAsync(new List<Purchase>() { purchase });
            var ret = await purchaseService.GetUserPurchases(a => a.Email == "test@test.com");
            userRepository.Verify(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>()));
            purchaseRepository.Verify(a => a.FindAllAsync(It.IsAny<Expression<Func<Purchase, bool>>>()));
            Assert.That(ret, Is.Not.Null);
            Assert.That(ret.Count, Is.EqualTo(1));
        }

        [Test()]
        public async Task DeletePurchaseTest()
        {
            purchaseRepository.Setup(a => a.DeleteAsync(It.IsAny<Purchase>())).ReturnsAsync(1);
            var ret = await purchaseService.DeletePurchase(purchase);
            Assert.That(ret, Is.EqualTo(1));
        }

        [Test()]
        public async Task GetAllTest()
        {
            purchaseRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<Purchase>() { purchase });
            var ret = await purchaseService.GetAll();
            Assert.That(ret.Count, Is.EqualTo(1));
        }

        [Test()]
        public async Task GetCashbackTest()
        {
            purchase.Status = ECashbackStatus.Approved;
            purchase.CashbackValue = 1000;
            userRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User());
            purchaseRepository.Setup(a => a.FindAllAsync(It.IsAny<Expression<Func<Purchase, bool>>>())).ReturnsAsync(new List<Purchase>() { purchase, purchase });
            var ret = await purchaseService.GetCashback(a => a.Email == "test@test.com");
            Assert.That(ret, Is.EqualTo(2000));
        }

        [Test()]
        public async Task GetCashbackTestZero()
        {
            purchase.Status = ECashbackStatus.Approved;
            purchase.CashbackValue = 1000;
            purchaseRepository.Setup(a => a.FindAllAsync(It.IsAny<Expression<Func<Purchase, bool>>>())).ReturnsAsync(new List<Purchase>() { purchase, purchase });
            var ret = await purchaseService.GetCashback(a => a.Email == "test@test.com");
            Assert.That(ret, Is.EqualTo(0));
        } 

        #endregion [ Tests ]
    }
}