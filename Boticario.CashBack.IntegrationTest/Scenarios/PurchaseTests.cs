// ----------------------------------------
// <copyright file=PurchaseTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boticario.CashBack.IntegrationTest.Scenarios
{
    public class PurchaseTests
    {
        #region [ Static fields ]

        private static string token; 

        #endregion [ Static fields ]

        

        #region [ Constructors ]

        public PurchaseTests()
        {
        }

        #endregion [ Constructors ]

        #region [ Public methods ]

        public async Task<string> GetToken(TestProvider testServer)
        {
            if (token == null)
                token = await testServer.GetToken("user@boticario.com.br", "password");
            return await Task<string>.FromResult(token);
        }

        #endregion [ Public methods ]

        #region [ Tests ]


        [Fact]
        public async Task UserPostCreatePurchase()
        {
            using (var server = new TestProvider())
            using (var client = server.Client)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken(server));
                var parametro = new Purchase() { Cpf = "82177322057", Value = 6000, Email = "user@boticario.com.br" };
                var jsonContent = JsonConvert.SerializeObject(parametro);

                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("/api/Purchase", contentString);
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task UserGetGetPurchase()
        {
            using (var server = new TestProvider())
            using (var client = server.Client)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken(server));

                var contentString = new StringContent("", Encoding.UTF8, "application/json");

                var response = await client.GetAsync("/api/Purchase");
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        } 

        #endregion [ Tests ]

    }
}
