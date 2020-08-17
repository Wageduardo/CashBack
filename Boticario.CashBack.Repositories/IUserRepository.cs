// ----------------------------------------
// <copyright file=IUserRepository.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;

namespace Boticario.CashBack.Repositories
{
    /// <summary>
    /// User Repository
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Repositories.IRepository{Boticario.CashBack.Models.User}" />
    public interface IUserRepository : IRepository<User>
    {
    }
}