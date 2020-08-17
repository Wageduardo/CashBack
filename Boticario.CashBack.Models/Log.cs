// ----------------------------------------
// <copyright file=Log.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boticario.CashBack.Models
{
    /// <summary>
    /// Log database
    /// </summary>
    public class Log
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public UInt64 Id { get; set; }

        /// <summary>
        /// Gets or sets the created time.
        /// </summary>
        /// <value>
        /// The created time.
        /// </value>
        public DateTime CreatedTime { get; set; } = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the thread.
        /// </summary>
        /// <value>
        /// The thread.
        /// </value>
        public string Thread { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the thread.
        /// </summary>
        /// <value>
        /// The name of the thread.
        /// </value>
        public string ThreadName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <value>
        /// The name of the machine.
        /// </value>
        public string MachineName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public string Exception { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the stacktrace.
        /// </summary>
        /// <value>
        /// The stacktrace.
        /// </value>
        public string Stacktrace { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the stacktrace.
        /// </summary>
        /// <value>
        /// The stacktrace.
        /// </value>
        public string ExceptionStacktrace { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public string Identity { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the callsite.
        /// </summary>
        /// <value>
        /// The callsite.
        /// </value>
        public string Callsite { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; } = string.Empty;

        #endregion [ Properties ]
    }
}
