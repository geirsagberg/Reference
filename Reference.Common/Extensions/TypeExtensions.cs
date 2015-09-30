using System;
using System.Reflection;
using Reference.Common.Exceptions;

namespace Reference.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>() != null;
        }

        public static bool Implements<T>(this Type type)
        {
            var iType = typeof (T);
            if (!iType.IsInterface)
                throw new TypeArgumentException<T>("Only interfaces can be implemented.");
            return iType.IsAssignableFrom(type);
        }

        public static bool Inherits<T>(this Type type)
        {
            var iType = typeof (T);
            if (iType.IsInterface)
                throw new TypeArgumentException<T>("Interfaces cannot be inherited from. Use Implements<T>");
            return iType.IsAssignableFrom(type);
        }

        public static bool IsNullableType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsNotNullableType(this Type type)
        {
            return !IsNullableType(type);
        }
    }
}