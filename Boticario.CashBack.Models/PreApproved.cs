// ----------------------------------------
// <copyright file=PreApproved.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System.ComponentModel.DataAnnotations;

namespace Boticario.CashBack.Models
{
    /// <summary>
    /// This class is to PreApproved clients
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Models.EntityBase" />
    public class PreApproved : EntityBase
    {

        /// <summary>
        /// Gets or sets the CPF.
        /// </summary>
        /// <value>
        /// The CPF.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the CPF"),  RegularExpression("([0-9]{1,11})", ErrorMessage = "Numbers only - Maximum size 11")]
        public string Cpf { get; set; }
    }
}
