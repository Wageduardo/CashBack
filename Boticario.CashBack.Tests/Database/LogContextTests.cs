// ----------------------------------------
// <copyright file=LogContextTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Tests.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Boticario.CashBack.Repositories.Database.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class LogContextTests
    {
        #region [ Fields ]

        LogContext context;
        string file = Guid.NewGuid().ToString();

        #endregion [ Fields ]
        
        #region [ Arrange ]


        [SetUp()]
        public void Initiliaze()
        {
            var options = new DbContextOptionsBuilder<LogContext>()
                .UseSqlite($"Data Source = {file}.db")
                .Options;



            context = new LogContext(options);

            var log = new Log();
            log.SetAllProperties();
            log.GetPropertyValues();
            log.ToString();
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
        public void CreateDataBaseTest()
        {
            context.CreateDataBase();
            Assert.IsTrue(File.Exists($"{file}.db"));
        } 

        #endregion [ Tests ]
    }
}