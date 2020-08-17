// ----------------------------------------
// <copyright file=LogTest.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Boticario.CashBack.IntegrationTest.Scenarios
{
    /// <summary>
    /// Logs Tests
    /// </summary>
    public class LogTest
    {
        #region [ Static fields ]

        private static string token; 

        #endregion [ Static fields ]

        #region [ Constructors ]

        public async Task<string> GetToken(TestProvider testServer)
        {
            if (token == null)
                token = await testServer.GetToken("admin@boticario.com.br", "password");
            return await Task<string>.FromResult(token);
        }

        #endregion [ Constructors ]

        #region [ Tests ]

        [Fact]
        public async Task GetLogsTest()
        {
            using (var server = new TestProvider())
            using (var client = server.Client)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken(server));
                var responseMessage = await client.GetAsync("/api/Log");
                responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        } 

        #endregion [ Tests ]
    }
}

