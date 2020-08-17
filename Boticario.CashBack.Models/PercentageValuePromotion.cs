// ----------------------------------------
// <copyright file=PercentageValuePromotion.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
namespace Boticario.CashBack.Models
{
    /// <summary>
    /// This is to percentage-value promotion 
    /// e.g: If the purchase amount is greater than the amount, the discount percentage applies
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Models.EntityBase" />
    public class PercentageValuePromotion : EntityBase
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public double Percentage { get; set; }
    }
}