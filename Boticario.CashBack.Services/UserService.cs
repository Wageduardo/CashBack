// ----------------------------------------
// <copyright file=UserService.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core;
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Boticario.CashBack.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Services.IUserService" />
    public class UserService : IUserService
    {
        #region [ Fields ]

        private readonly IUserRepository userRepository; 

        #endregion [ Fields ]

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        } 

        #endregion [ Constructors ]

        #region [ Public methods ]

        /// <summary>
        /// Authenticates the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<(User user, string token)> Authenticate(string email, string password)
        {
            User user = await GetUserByEmail(email, false);
            if (user == null)
            {
                return (null, "");
            }
            if (!user.Password.VerifyHashPassword(password))
            {
                return (null, "");
            }
            var token = GenerateToken(user);
            return (user, token);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// User E-mail already exists
        /// or
        /// User CPF already exists
        /// </exception>
        public async Task<User> CreateUser(User user)
        {
            ValideteUserData(user);
            var userTmp = await GetUserByEmail(user.Email);
            if (userTmp != null)
                throw new InvalidOperationException("User E-mail already exists");

            return await userRepository.AddAsync(user);
        }


        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">User with id: {user.Id} does not exists. Could not delete the user.</exception>
        public async Task<User> DeleteUser(User user)
        {
            var count = await userRepository.DeleteAsync(user);
            if (count == 0)
                throw new InvalidOperationException($"User with id: {user.Id} does not exists. Could not delete the user.");
            return user;
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAll()
        {
            return await userRepository.GetAllAsync();
        }


        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">User does not exists.</exception>
        public async Task<User> GetUser(Guid id)
        {
            var ret = await userRepository.GetByIdAsync(id);
            if (ret == null)
                throw new InvalidOperationException($"User does not exists.");
            return ret;
        }


        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">E-mail is mandatory</exception>
        public async Task<User> GetUserByEmail(string email, bool validate = true)
        {
            if (validate && string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("E-mail is mandatory");
            }
            return await userRepository.FindAsync(a => a.Email.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public async Task<User> FindUser(Expression<Func<User, bool>> match)
        {
            return await userRepository.FindAsync(match);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">User with id: {user.Id} does not exists. Could not update the user.</exception>
        public async Task<User> UpdateUser(User user)
        {
            var ret = await GetUser(user.Id) ??
                throw new InvalidOperationException($"User with id: {user.Id} does not exists. Could not update the user.");

            await userRepository.UpdateAsync(user, user.Id);
            return ret;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public string GenerateToken(User user)
        {
            var key = AppConfig.Key;
            var expiration = Convert.ToInt32(AppConfig.Expiration);
            var token = new JwtSecurityToken(
                issuer: AppConfig.Issuer,
                audience: AppConfig.Audience,
                expires: DateTime.Now.AddHours(expiration),
                signingCredentials: GetCredentials(key),
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validetes the user data.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="InvalidOperationException">
        /// User E-mail is mandatory
        /// or
        /// User name is mandatory
        /// or
        /// User Password is mandatory
        /// or
        /// User CPF is mandatory
        /// or
        /// User Role is mandatory
        /// or
        /// User E-mail already exists
        /// or
        /// User CPF already exists
        /// </exception>
        public void ValideteUserData(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
                throw new InvalidOperationException("User E-mail is mandatory");

            if (string.IsNullOrEmpty(user.Name))
                throw new InvalidOperationException("User Name is mandatory");

            if (string.IsNullOrEmpty(user.Password))
                throw new InvalidOperationException("User Password is mandatory");
            else
                user.Password = user.Password.ToHashPassword();

            if (string.IsNullOrEmpty(user.Cpf))
                throw new InvalidOperationException("User CPF is mandatory");
            else
                user.Cpf = user.Cpf.Replace(".", "").Replace("-", "");

            if (string.IsNullOrEmpty(user.Role))
                throw new InvalidOperationException("User Role is mandatory");

        }


        #endregion [ Public methods ]

        #region [ Private methods ]


        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <param name="asymetricKey">The asymetric key.</param>
        /// <returns></returns>
        private SigningCredentials GetCredentials(string asymetricKey)
        {
            var bytes = Encoding.ASCII.GetBytes(asymetricKey);
            var key = new SymmetricSecurityKey(bytes);
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        } 

        #endregion [ Private methods ]
    }
}
