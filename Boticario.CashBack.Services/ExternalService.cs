// ----------------------------------------
// <copyright file=ExternalService.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core;
using Boticario.CashBack.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    public class ExternalService : IExternalService
    {
        private readonly ILogger<ExternalService> logger;
        private readonly IHttpClientHelper httpClientHelper;

        public ExternalService(ILogger<ExternalService> logger,
                               IHttpClientHelper httpClientHelper)
        {
            this.logger = logger;
            this.httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// Gets the cert data.
        /// </summary>
        /// <param name="cpf">The CPF.</param>
        /// <returns></returns>
        public async Task<ExternalViewModel> GetCashback(string cpf)
        {
            try
            {
                string baseUrl = $"{AppConfig.ExternalUrl}v1/cashback?cpf={cpf}";
                return await httpClientHelper.SendGetAsync<ExternalViewModel>(baseUrl, AppConfig.ExternalToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}