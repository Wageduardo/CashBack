// ----------------------------------------
// <copyright file=AuthResponse.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System;

namespace Boticario.CashBack.Models.ViewModel
{
    /// <summary>
    /// ViewModel
    /// </summary>
    public class AuthResponse
    {
        #region [ Properties ]

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }
        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; }
        /// <summary>
        /// Gets the CPF.
        /// </summary>
        /// <value>
        /// The CPF.
        /// </value>
        public string Cpf { get; }
        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; }

        #endregion [ Properties ]

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthResponse"/> class.
        /// </summary>
        public AuthResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthResponse"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        public AuthResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Cpf = user.Cpf;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        } 

        #endregion [ Constructors ]
    }
}
