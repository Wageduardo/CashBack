// ----------------------------------------
// <copyright file=IHttpClientHelper.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    public interface IHttpClientHelper
    {
        Task<TResult> SendGetAsync<TResult>(string url, string token);
    }
}
