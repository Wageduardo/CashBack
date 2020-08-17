// ----------------------------------------
// <copyright file=PurchaseRepository.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories.Database;

namespace Boticario.CashBack.Repositories
{
    /// <summary>
    /// Purchase Repository
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Repositories.Repository{Boticario.CashBack.Models.Purchase}" />
    /// <seealso cref="Boticario.CashBack.Repositories.IPurchaseRepository" />
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(BoticarioContext context) : base(context)
        {
        }
    }
}
