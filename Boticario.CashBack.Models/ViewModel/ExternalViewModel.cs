// ----------------------------------------
// <copyright file=ExternalViewModel.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
namespace Boticario.CashBack.Models.ViewModel
{
    /// <summary>
    /// External model
    /// </summary>
    public class ExternalViewModel
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; }
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public Body Body { get; set; } 

        #endregion [ Properties ]
    }

    /// <summary>
    /// Body to External model
    /// </summary>
    public class Body
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the credit.
        /// </summary>
        /// <value>
        /// The credit.
        /// </value>
        public decimal Credit { get; set; } 

        #endregion [ Properties ]
    }
}
