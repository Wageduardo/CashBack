// ----------------------------------------
// <copyright file=Purchase.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Interfaces.Models;
using Boticario.CashBack.Models.Interfaces.Enums;
using System.ComponentModel.DataAnnotations;

namespace Boticario.CashBack.Models
{
    /// <summary>
    /// /// This is to purchase data
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Models.EntityBase" />
    /// <seealso cref="Boticario.CashBack.Interfaces.Models.IPurchase" />
    public class Purchase : EntityBase, IPurchase
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the CPF.
        /// </summary>
        /// <value>
        /// The CPF.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the CPF"), RegularExpression("([0-9]{1,11})", ErrorMessage = "Numbers only - Maximum size 11")]
        public string Cpf { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        [Range(0, 999999999.99)]
        public double Value { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the E-mail")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the cashback percent.
        /// </summary>
        /// <value>
        /// The cashback percent.
        /// </value>
        public double? CashbackPercent { get; set; }
        /// <summary>
        /// Gets or sets the cashback value.
        /// </summary>
        /// <value>
        /// The cashback value.
        /// </value>
        public double? CashbackValue { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ECashbackStatus Status { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns> 

        #endregion [ Properties ]

        #region [ Public methods ]

        public override string ToString()
        {
            return base.ToString() + $"[CPF={Cpf}] - [Value={Value}] - [Email={Email} - [Code={Code}] - [CashbackPercent={CashbackPercent}] - [CashbackValue={CashbackValue}] - [Status={Status}]";
        } 

        #endregion [ Public methods ]
    }
}
