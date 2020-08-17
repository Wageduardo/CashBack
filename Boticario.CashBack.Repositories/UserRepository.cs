// ----------------------------------------
// <copyright file=UserRepository.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories.Database;

namespace Boticario.CashBack.Repositories
{
    /// <summary>
    /// UserRepository
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Repositories.Repository{Boticario.CashBack.Models.User}" />
    /// <seealso cref="Boticario.CashBack.Repositories.IUserRepository" />
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BoticarioContext context) : base(context)
        {
        }
    }
}
