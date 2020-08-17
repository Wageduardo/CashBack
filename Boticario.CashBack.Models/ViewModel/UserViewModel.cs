// ----------------------------------------
// <copyright file=UserViewModel.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System;

namespace Boticario.CashBack.Models.ViewModel
{
    public class UserViewModel
    {
        #region [ Properties ]

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        #endregion [ Properties ]

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Cpf = user.Cpf;
            Email = user.Email;
            Role = user.Role;
        } 

        #endregion [ Constructors ]

    }
}
