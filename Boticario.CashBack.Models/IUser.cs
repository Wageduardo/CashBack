// ----------------------------------------
// <copyright file=IUser.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
namespace Boticario.CashBack.Interfaces.Models
{
    public interface IUser : IEntityBase
    {
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        string Role { get; set; }

        string ToString();
    }
}