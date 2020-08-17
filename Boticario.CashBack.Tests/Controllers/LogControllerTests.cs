// ----------------------------------------
// <copyright file=LogControllerTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Repositories.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class LogControllerTests
    {
        #region [ Fields ]

        private Mock<ILogger<LogController>> logger;
        private LogContext context;
        private LogController logController; 

        #endregion [ Fields  ]

        #region [ Arrange ]

        [SetUp()]
        public void Initiliaze()
        {
            var options = new DbContextOptionsBuilder<LogContext>()
                .UseSqlite($"Data Source = {Guid.NewGuid()}.db")
                .Options;

            context = new LogContext(options);
            logger = new Mock<ILogger<LogController>>();
            logController = new LogController(logger.Object);
        }

        [TearDown()]
        public void TearDown()
        {
            var db = context.Database;
            context.Dispose();
            db.EnsureDeleted();
        }

        #endregion [ Arrange ]

        #region [ Tests ]

        [Test()]
        public async Task GetTest()
        {
            var result = await logController.Get(context);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = result as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
        }

        [Test()]
        public async Task GetTestEx()
        {
            context.Database.ExecuteSqlRaw("DROP TABLE logs");
            var result = await logController.Get(context);
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var ret = result as BadRequestObjectResult;
            Assert.That(ret, Is.Not.Null);
        } 

        #endregion [ Tests  ]
    }
}