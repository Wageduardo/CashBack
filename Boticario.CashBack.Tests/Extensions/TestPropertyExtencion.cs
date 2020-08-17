// ----------------------------------------
// <copyright file=TestPropertyExtencion.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Boticario.CashBack.Tests.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class TestPropertyExtension
    {
        /// <summary>
        /// Gets the property values.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void GetPropertyValues(this Object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length > 0)
                {
                    Console.WriteLine("   {0} ({1}):", prop.Name, prop.PropertyType.Name);
                }
                else
                {
                    Console.WriteLine("   {0} ({1}): {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(obj));
                }
            }
        }

        /// <summary>
        /// Sets all properties.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void SetAllProperties(this Object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (var propertyInfo in props)
            {
                if (propertyInfo.CanWrite)
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(obj, string.Empty, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(obj, false, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(short))
                    {
                        propertyInfo.SetValue(obj, (short)0, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(obj, 0, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(long))
                    {
                        propertyInfo.SetValue(obj, 0, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(double))
                    {
                        propertyInfo.SetValue(obj, 0.0, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        propertyInfo.SetValue(obj, DateTime.MinValue, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(Guid))
                    {
                        propertyInfo.SetValue(obj, Guid.Empty, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(Enum))
                    {
                        propertyInfo.SetValue(obj, null, null);
                        continue;
                    }
                    if (propertyInfo.PropertyType == typeof(object))
                    {
                        propertyInfo.SetValue(obj, null, null);
                    }
                }
            }
        }
    }
}
