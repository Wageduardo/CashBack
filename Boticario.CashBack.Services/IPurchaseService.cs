// ----------------------------------------
// <copyright file=IPurchaseService.cs company=Boticario>
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
    /// Purchase Service
    /// </summary>
    public interface IPurchaseService
    {
        /// <summary>
        /// Creates the purchase.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<Purchase> CreatePurchase(Purchase purchase, User user);
        /// <summary>
        /// Deletes the purchase.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <returns></returns>
        Task<int> DeletePurchase(Purchase purchase);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<List<Purchase>> GetAll();
        /// <summary>
        /// Gets the cashback.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<double> GetCashback(Expression<Func<User, bool>> match);
        /// <summary>
        /// Gets the caskback percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task<double> GetCaskbackPercent(double value);
        /// <summary>
        /// Gets the purchase.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Purchase> GetPurchase(Guid id);
        /// <summary>
        /// Gets the user purchases.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<List<Purchase>> GetUserPurchases(Expression<Func<User, bool>> match);
        /// <summary>
        /// Updates the purchase.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<Purchase> UpdatePurchase(Purchase purchase, User user);
        /// <summary>
        /// Validates the mandatory fields.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <param name="user">The user.</param>
        void ValidateMandatoryFields(Purchase purchase, User user);
    }
}