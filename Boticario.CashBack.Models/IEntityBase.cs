// ----------------------------------------
// <copyright file=IEntityBase.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System;

namespace Boticario.CashBack.Interfaces.Models
{
    public interface IEntityBase : ICloneable
    {

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        DateTime CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }


        /// <summary>
        /// Gets or sets the modified time.
        /// </summary>
        /// <value>
        /// The modified time.
        /// </value>
        DateTime? ModifiedTime { get; set; }
    }
}
