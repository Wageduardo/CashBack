// ----------------------------------------
// <copyright file=PurchaseService.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.Interfaces.Enums;
using Boticario.CashBack.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    /// <summary>
    /// Purchase Service
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Services.IPurchaseService" />
    public class PurchaseService : IPurchaseService
    {
        #region [ Fields ]

        private readonly IPurchaseRepository purchaseRepository;
        private readonly IRepository<PercentageValuePromotion> percentageValuePromotionRepository;
        private readonly IRepository<PreApproved> preApprovedRpository;
        private readonly IUserRepository userRepository;

        #endregion [ Fields ]
        
        #region [ Constructors ]


        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseService"/> class.
        /// </summary>
        /// <param name="purchaseRepository">The purchase repository.</param>
        /// <param name="percentageValuePromotionRepository">The percentage value promotion repository.</param>
        /// <param name="preApprovedRpository">The pre approved rpository.</param>
        /// <param name="userRepository">The user repository.</param>
        public PurchaseService(IPurchaseRepository purchaseRepository,
                               IRepository<PercentageValuePromotion> percentageValuePromotionRepository,
                               IRepository<PreApproved> preApprovedRpository,
                               IUserRepository userRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.percentageValuePromotionRepository = percentageValuePromotionRepository;
            this.preApprovedRpository = preApprovedRpository;
            this.userRepository = userRepository;
        }

        #endregion [ Constructors ]

        #region [ Public methods ]

        /// <summary>
        /// Creates the purchase.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <returns></returns>
        public async Task<Purchase> CreatePurchase(Purchase purchase, User user)
        {
            ValidateMandatoryFields(purchase, user);
            await SetCashback(purchase);
            purchase.Status = await GetStatus(purchase.Cpf);
            purchase.Code = await GetCode();
            return await purchaseRepository.AddAsync(purchase);
        }

        /// <summary>
        /// Deletes the purchase.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeletePurchase(Purchase purchase)
        {
            return await purchaseRepository.DeleteAsync(purchase);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Purchase>> GetAll()
        {
            return await purchaseRepository.GetAllAsync();
        }

        /// <summary>
        /// Gets the purchases.
        /// </summary>
        /// <param name="match">The email.</param>
        /// <returns></returns>
        public async Task<List<Purchase>> GetUserPurchases(Expression<Func<User, bool>> match)
        {
            var loggedUser = await userRepository.FindAsync(match);
            if (loggedUser != null)
            {
                return await purchaseRepository.FindAllAsync(a => a.Email == loggedUser.Email);
            }
            return null;
        }

        /// <summary>
        /// Gets the purchase.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Purchase> GetPurchase(Guid id)
        {
            return await purchaseRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Updates the purchase.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <returns></returns>
        public async Task<Purchase> UpdatePurchase(Purchase purchase, User user)
        {
            var currentPurchase = await purchaseRepository.GetByIdAsync(purchase.Id);
            if (currentPurchase == null)
            {
                return null;
            }
            ValidateMandatoryFields(purchase, user);
            await SetCashback(purchase);
            return await purchaseRepository.UpdateAsync(purchase, currentPurchase.Id);
        }

        /// <summary>
        /// Gets the cask-back percent.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public async Task<double> GetCaskbackPercent(double value)
        {
            foreach (var item in (await percentageValuePromotionRepository.GetAllAsync()).OrderByDescending(a => a.Value))
            {
                if (value > item.Value)
                    return Math.Round(item.Percentage, 2);
            }
            return 0;
        }

        /// <summary>
        /// Validates the mandatory fields.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        /// <exception cref="InvalidOperationException">
        /// CPF is mandatory
        /// or
        /// Code is mandatory
        /// or
        /// Email is mandatory
        /// </exception>
        public void ValidateMandatoryFields(Purchase purchase, User user)
        {



            if (string.IsNullOrEmpty(purchase.Cpf))
                throw new InvalidOperationException("CPF is mandatory");
            else
                purchase.Cpf = purchase.Cpf.Trim().Replace("-", "").Replace(".", "");

            if (!purchase.Cpf.All(char.IsDigit))
                throw new InvalidOperationException("CPF is invalid use only numbers");

            if (string.IsNullOrEmpty(purchase.Email))
                throw new InvalidOperationException("Email is mandatory");

            /// If is app where anyone can buy just remove this validation
            if (purchase.Cpf != user.Cpf)
            {
                throw new InvalidOperationException($"Cannot create purchase with CPF different from your {user.Cpf}");
            }

            /// If is app where anyone can buy just remove this validation
            if (purchase.Email.ToLower() != user.Email.ToLower())
            {
                throw new InvalidOperationException($"Cannot create purchase with E-mail different from your {user.Email}");
            }
        }

        /// <summary>
        /// Gets the cashback.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<double> GetCashback(Expression<Func<User, bool>> match)
        {
            double ret = 0;
            try
            {
                ret = (double)(await GetUserPurchases(match)).Where(a => a.Status == ECashbackStatus.Approved).Sum(a => a.CashbackValue);
            }
            catch (Exception)
            {
                //// Avoid cast issues
            }
            return ret;
        }

        #endregion [ Public methods ]

        #region [ Private methods ]

        /// <summary>
        /// Sets the cash-back.
        /// </summary>
        /// <param name="purchase">The purchase.</param>
        private async Task SetCashback(Purchase purchase)
        {
            var auxPercent = await GetCaskbackPercent(purchase.Value);
            purchase.CashbackPercent = auxPercent;
            purchase.CashbackValue = Math.Round(purchase.Value * auxPercent / 100, 2);
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="cpf">The CPF.</param>
        /// <returns></returns>
        private async Task<ECashbackStatus> GetStatus(string cpf)
        {
            if (await preApprovedRpository.AnyAsync(a => a.Cpf == cpf))
                return ECashbackStatus.Approved;
            return ECashbackStatus.OnChecking;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetCode()
        {
            Random rnd = new Random();
            byte[] bytes = new byte[8];
            rnd.NextBytes(bytes);
            string ret = BitConverter.ToString(bytes).Replace("-", "");
            if (await purchaseRepository.AnyAsync(a => a.Code == ret))
                return await GetCode();
            return ret;
        } 

        #endregion [ Private methods ]
    }
}
