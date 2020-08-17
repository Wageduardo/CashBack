// ----------------------------------------
// <copyright file=UserTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boticario.CashBack.IntegrationTest.Scenarios
{
    public class UserTests
    {
        #region [ Static fields ]

        private static string token;

        #endregion [ Static fields ]

        #region [ constructors ]


        public UserTests()
        {

        }


        #endregion [ constructors ]

        #region [ Public methods ]

        public async Task<string> GetToken(TestProvider testServer)
        {
            if (token == null)
                token = await testServer.GetToken("admin@boticario.com.br", "password");
            return await Task<string>.FromResult(token);
        }

        #endregion [ Public methods ]

        #region [ Tests ]

        [Fact]
        public async Task UserPostCreateNewUser()
        {
            using (var server = new TestProvider())
            using (var client = server.Client)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken(server));
                var parametro = new User() { Cpf = new Random().Next(100, 100000).ToString(), Name = "Ellie Tlou", Email = $"ellie_{Guid.NewGuid().ToString().Split("-").First()}@boticario.com.br", Password = "password", Role = "User" };
                var jsonContent = JsonConvert.SerializeObject(parametro);

                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("/api/User", contentString);
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Fact]
        public async Task UserGetAllUser()
        {
            using (var server = new TestProvider())
            using (var client = server.Client)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken(server));

                var contentString = new StringContent("", Encoding.UTF8, "application/json");

                var response = await client.GetAsync("/api/User");
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        } 

        #endregion [ Tests ]
    }
}
