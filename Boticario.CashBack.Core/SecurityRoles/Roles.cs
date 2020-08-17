// ----------------------------------------
// <copyright file=Roles.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
namespace Boticario.CashBack.Api.Extensions.SecurityRoles
{

    /// <summary>
    /// /// Policies for Application
    /// </summary>
    public static class Roles
    {
        #region [ Constants ]

        public const string ReadOnly = "ReadOnly";
        public const string OperationalUser = "User";
        public const string SolutionAdministrator = "Administrator"; 

        #endregion [ Constants ]
    }
}
