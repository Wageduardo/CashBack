// ----------------------------------------
// <copyright file=JsonConverterExtesion.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System
{
    /// <summary>
    /// Aux to convert JSON to Object and vise versa.
    /// </summary>
    public static class  JsonConverterExtension
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="formatting">The formatting.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToJson<T>(this T obj, Formatting formatting = Formatting.None)
        {
            if (obj == null)
                throw new ArgumentNullException($"{nameof(obj)} - { typeof(T) }");

            return JsonConvert.SerializeObject(obj, formatting, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T FromJson<T>(this string obj, Type type)
        {
            if (obj == null)
                throw new ArgumentNullException($"{nameof(obj)} - { typeof(T)}");

            return (T)JsonConvert.DeserializeObject(obj, type);
        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="jsonSerializerSettings">The json serializer settings.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T FromJson<T>(this string obj, JsonSerializerSettings jsonSerializerSettings = null)
        {
            if (obj == null)
                throw new ArgumentNullException($"{nameof(obj)} - { typeof(T)}");

            if (jsonSerializerSettings != null)
            {
                return JsonConvert.DeserializeObject<T>(obj, jsonSerializerSettings);
            }

            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
