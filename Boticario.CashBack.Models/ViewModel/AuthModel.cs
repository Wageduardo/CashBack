// ----------------------------------------
// <copyright file=AuthModel.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System.ComponentModel.DataAnnotations;

namespace Boticario.CashBack.Models.ViewModel
{
    /// <summary>
    /// ViewModel
    /// </summary>
    public class AuthModel
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the E-mail"), EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the Password")]
        public string Password { get; set; } 

        #endregion [ Properties ]
    }
}
