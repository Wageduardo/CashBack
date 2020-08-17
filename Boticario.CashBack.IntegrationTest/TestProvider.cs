// ----------------------------------------
// <copyright file=TestProvider.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Api;
using Boticario.CashBack.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Boticario.CashBack.IntegrationTest
{
    /// <summary>
    /// Locke create to avoid issues when generate database
    /// </summary>
    public static class LockCreate
    {
        public static object Lock = new object();
    }

    /// <summary>
    ///  Test provider
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class TestProvider : IDisposable
    {
        #region [ Fields ]

        private readonly TestServer server;

        #endregion [ Fields ]

        #region [ Properties ]        
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public HttpClient Client { get; private set; }

        #endregion [ Properties ]


        #region [ Constructors ]        
        /// <summary>
        /// Initializes a new instance of the <see cref="TestProvider"/> class.
        /// </summary>
        public TestProvider()
        {
            lock (LockCreate.Lock)
            {
                server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
                Client = server.CreateClient();
            }
        }

        #endregion [ Constructors ]

        #region [ Public methods ]        
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<string> GetToken(string email, string password)
        {
            using (var client = new TestProvider().Client)
            {
                var authModel = new AuthModel
                {
                    Email = email,
                    Password = password
                };

                var jsonContent = JsonConvert.SerializeObject(authModel);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                contentString.Headers.ContentType = new
                MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("/api/Auth", contentString);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic item = JsonConvert.DeserializeObject<object>(responseString);
                return item["token"];
            }
        }


        #endregion [ Public methods ]

        #region [ Dispose ]


        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        } 

        #endregion [ Dispose ]
    }
}
