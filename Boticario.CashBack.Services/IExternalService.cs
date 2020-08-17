// ----------------------------------------
// <copyright file=IExternalService.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models.ViewModel;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    public interface IExternalService
    {
        Task<ExternalViewModel> GetCashback(string cpf);
    }
}