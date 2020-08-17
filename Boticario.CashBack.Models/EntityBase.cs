// ----------------------------------------
// <copyright file=EntityBase.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Interfaces.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boticario.CashBack.Models
{
    /// <summary>
    /// Basis for entities
    /// </summary>
    /// <seealso cref="Boticario.CashBack.Interfaces.Models.IEntityBase" />
    public class EntityBase : IEntityBase
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        /// [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        /// [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ModifiedTime { get; set; }



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
            return GetType().Name + $" [Id={Id}] - [CreatedAt={CreatedTime}] - [ModifiedTime={ModifiedTime}]";
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        } 

        #endregion [ Public methods ]
    }
}