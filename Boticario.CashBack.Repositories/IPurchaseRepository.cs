// ----------------------------------------
// <copyright file=IPurchaseRepository.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;

namespace Boticario.CashBack.Repositories
{
    /// <summary>
    /// Purchase Repository
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Repositories.IRepository{Boticario.CashBack.Models.Purchase}" />
    public interface IPurchaseRepository : IRepository<Purchase>
    {
    }
}