// ----------------------------------------
// <copyright file=AppConfig.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
namespace Boticario.CashBack.Core
{
    /// <summary>
    /// Class was created to avoid Github appsettings.json issues
    /// </summary>
    public static class AppConfig
    {
        public static string Key { get; } = "1f0a5c5cd14a48ef90b7e3c129f69ab27VNy59nHvkibjswFOYhZAAb74ade889c2e4ecda2a896c339342e4c";
        public static string Issuer { get; } = "Boticario";
        public static string Audience { get; } = "Boticario";
        public static int Expiration { get; } = 24;
        public static int RefreshExpiration { get; } = 60;
        public static string SqliteConnectionString { get; } = "Data Source=BoticarioDb.db";

        public static string ExternalUrl { get; } = "https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/";
        public static string ExternalToken { get; } = "ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm";
        public static string LogDb { get; } = "Data Source = LogDb.db";
    }
}