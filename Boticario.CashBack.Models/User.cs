// ----------------------------------------
// <copyright file=User.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace Boticario.CashBack.Models
{
    /// <summary>
    /// This is to user data
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Models.EntityBase" />
    /// <seealso cref="Boticario.CashBack.Interfaces.Models.IUser" />
    public class User : EntityBase, IUser
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the Name")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the CPF.
        /// </summary>
        /// <value>
        /// The CPF.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the CPF"), RegularExpression("([0-9]{1,11})", ErrorMessage = "Numbers only - Maximum size 11")]
        public string Cpf { get; set; }
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

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the Role - ('Administrator' or 'User')")]
        public string Role { get; set; }

        #endregion [ Properties ]

        #region [ Public methods ]

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + $"[Name={Name}] - [CPF={Cpf}] - [Email={Email}] - [Role={Role}]";
        } 

        #endregion [ Public methods ]
    }
}
