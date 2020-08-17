// ----------------------------------------
// <copyright file=ExternalServiceTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models.ViewModel;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class ExternalServiceTests
    {

        #region [ Fields ]

        Mock<ILogger<ExternalService>> logger;
        Mock<IHttpClientHelper> httpClientHelper;
        ExternalService externalService;

        #endregion [ Fields ]


        #region [ Arrange ]

        [SetUp]
        public void Init()
        {
            logger = new Mock<ILogger<ExternalService>>();
            httpClientHelper = new Mock<IHttpClientHelper>();
            externalService = new ExternalService(logger.Object, httpClientHelper.Object);
        }

        #endregion [ Arrange  ]

        #region [ Tests ]

        [Test()]
        public async Task GetCashbackTest()
        {
            httpClientHelper.Setup(a => a.SendGetAsync<ExternalViewModel>(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ExternalViewModel()
                {
                    StatusCode = 200,
                    Body = new Body()
                    {
                        Credit = 200
                    }
                });

            var a = await externalService.GetCashback("123123123");
            Assert.That(a, Is.Not.Null);
            Assert.That(a.StatusCode, Is.EqualTo(200));
        }

        [Test()]
        public async Task GetCashbackTestEx()
        {
            httpClientHelper.Setup(a => a.SendGetAsync<ExternalViewModel>(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var a = await externalService.GetCashback("asd");
            Assert.That(a, Is.Null);
        } 

        #endregion [ Tests  ]
    }
}