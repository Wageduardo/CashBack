// ----------------------------------------
// <copyright file=IUserService.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<(User user, string token)> Authenticate(string email, string password);
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<User> CreateUser(User user);
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<User> DeleteUser(User user);
        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<User> UpdateUser(User user);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAll();
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<User> GetUser(Guid id);
        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="validate">if set to <c>true</c> [validate].</param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email, bool validate = true);
        /// <summary>
        /// Validetes the user data.
        /// </summary>
        /// <param name="user">The user.</param>
        void ValideteUserData(User user);
        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<User> FindUser(Expression<Func<User, bool>> match);
    }
}