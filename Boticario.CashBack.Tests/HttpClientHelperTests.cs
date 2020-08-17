// ----------------------------------------
// <copyright file=HttpClientHelperTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core;
using Boticario.CashBack.Models.ViewModel;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class HttpClientHelperTests
    {
        #region [ Fields ]

        Mock<ILogger<HttpClientHelper>> logger;
        IHttpClientHelper httpClientHelper;

        #endregion [ Fields ]

        #region [ Arrange ]

        [SetUp]
        public void Init()
        {
            logger = new Mock<ILogger<HttpClientHelper>>();
            httpClientHelper = new HttpClientHelper(logger.Object);
        }

        #endregion [ Arrange ]


        #region [ Tests ]

        [Test()]
        public async Task SendGetAsyncTest()
        {
            string cpf = "123";
            string baseUrl = $"{AppConfig.ExternalUrl}v1/cashback?cpf={cpf}";
            var a = await httpClientHelper.SendGetAsync<ExternalViewModel>(baseUrl, AppConfig.ExternalToken);
            Assert.That(a, Is.Not.Null);
            Assert.That(a.StatusCode, Is.EqualTo(200));
        }

        [Test()]
        public async Task SendGetAsyncTestFails()
        {
            string cpf = "123";
            string baseUrl = $"{AppConfig.ExternalUrl}v1?cpf={cpf}";
            Assert.ThrowsAsync<HttpRequestException>(() => httpClientHelper.SendGetAsync<ExternalViewModel>(baseUrl, "123"));
        } 

        #endregion [ Tests ]
    }
}