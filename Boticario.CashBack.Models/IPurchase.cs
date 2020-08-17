// ----------------------------------------
// <copyright file=IPurchase.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models.Interfaces.Enums;

namespace Boticario.CashBack.Interfaces.Models
{
    public interface IPurchase : IEntityBase
    {
        /// <summary>
        /// Gets or sets the cashback percent.
        /// </summary>
        /// <value>
        /// The cashback percent.
        /// </value>
        double? CashbackPercent { get; set; }
        /// <summary>
        /// Gets or sets the cashback value.
        /// </summary>
        /// <value>
        /// The cashback value.
        /// </value>
        double? CashbackValue { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        string Code { get; set; }
        /// <summary>
        /// Gets or sets the CPF.
        /// </summary>
        /// <value>
        /// The CPF.
        /// </value>
        string Cpf { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        ECashbackStatus Status { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        double Value { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        string ToString();
    }
}