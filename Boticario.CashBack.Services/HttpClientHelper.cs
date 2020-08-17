// ----------------------------------------
// <copyright file=HttpClientHelper.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    public partial class HttpClientHelper : IHttpClientHelper
    {
        private readonly ILogger<HttpClientHelper> logger;

        public HttpClientHelper(ILogger<HttpClientHelper> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Sends the get message asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<TResult> SendGetAsync<TResult>(string url, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("token", token);

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorResponse = (await response.Content.ReadAsStringAsync()).FromJson<ErrorResponse>();
                    logger.LogWarning(errorResponse.Message);
                    throw new HttpRequestException(errorResponse.Message);
                }
                response.EnsureSuccessStatusCode();
                return (await response.Content.ReadAsStringAsync()).FromJson<TResult>();
            }
        }
    }
}
