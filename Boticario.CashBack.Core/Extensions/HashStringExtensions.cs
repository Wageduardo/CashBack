// ----------------------------------------
// <copyright file=HashStringExtensions.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace System
{
    /// <summary>
    /// Extension to hash string
    /// </summary>
    public static class HashStringExtensions
    {
        #region [ Public methods ]

        /// <summary>
        /// Converts to hashpassword.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns></returns>
        public static string ToHashPassword(this string phrase)
        {
            return Convert.ToBase64String(HashPassword(phrase));
        }

        /// <summary>
        /// Verifies the hash password.
        /// </summary>
        /// <param name="hashPassword">The hash password.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool VerifyHashPassword(this string hashPassword, string password)
        {
            if (string.IsNullOrEmpty(hashPassword) || string.IsNullOrEmpty(password))
                return false;
            return VerifyHashedPassword(Convert.FromBase64String(hashPassword), password);
        } 

        #endregion [ Public methods ]

        #region [ Private Methods ]

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private static byte[] HashPassword(string password)
        {
            return HashPassword(password,
                prf: KeyDerivationPrf.HMACSHA256,
                iterCount: 10000,
                saltSize: 128 / 8,
                numBytesRequested: 256 / 8);
        }

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="prf">The PRF.</param>
        /// <param name="iterCount">The iter count.</param>
        /// <param name="saltSize">Size of the salt.</param>
        /// <param name="numBytesRequested">The number bytes requested.</param>
        /// <returns></returns>
        private static byte[] HashPassword(string password, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return outputBytes;
        }

        /// <summary>
        /// Reads the network byte order.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

        /// <summary>
        /// Writes the network byte order.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="value">The value.</param>
        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        /// <summary>
        /// Verifies the hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private static bool VerifyHashedPassword(byte[] hashedPassword, string password)
        {
            int iterCount = default(int);

            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
                iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = hashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);

                return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }

        #endregion [ Private Methods ]
    }
}
