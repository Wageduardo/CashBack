// ----------------------------------------
// <copyright file=JsonConverterExtensionTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace System.Tests
{
    [TestFixture()]
    [ExcludeFromCodeCoverage]
    public class ObjectExtensionTests
    {
        #region [ Tests ]

        [Test()]
        public void FromToJsonTest()
        {
            var logDataLocation = new Log()
            {
                MachineName = Environment.MachineName,
                Id = 1,
                CreatedTime = DateTime.Now
            };

            string json = logDataLocation.ToJson<Log>();
            var logDataLocation1 = json.FromJson<Log>();
            Assert.AreEqual(logDataLocation.Id, logDataLocation1.Id);
            var logDataLocation2 = json.FromJson<Log>(typeof(Log));
            Assert.AreEqual(logDataLocation.Id, logDataLocation2.Id);
            var logDataLocation3 = json.FromJson<Log>(new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            Assert.AreEqual(logDataLocation.Id, logDataLocation3.Id);

            Assert.Throws<ArgumentNullException>(() => JsonConverterExtension.ToJson<Log>(null));
            Assert.Throws<ArgumentNullException>(() => JsonConverterExtension.FromJson<Log>(null));
            Assert.Throws<ArgumentNullException>(() => JsonConverterExtension.FromJson<Log>(null, typeof(Log)));
            Assert.Throws<ArgumentNullException>(() => JsonConverterExtension.FromJson<Log>(null, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        } 

        #endregion [ Tests ]
    }
}