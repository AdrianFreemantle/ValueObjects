using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ValueObjects
{
    internal static class ObjectFactory
    {
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;
        public static IEnumerable<TValueType> CreateInstances<TValueType, TEnum>()
            where TValueType : struct
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(t => CreateInstance<TValueType>(t))
                .ToArray();
        }

        private static T CreateInstance<T>(params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), parameters);
        }

        private static object CreateInstance(Type type, params object[] parameters)
        {
            Type[] types = parameters.Length == 0
                ? new Type[0]
                : parameters.ToList().ConvertAll(input => input.GetType()).ToArray();

            var constructor = type.GetConstructor(Flags, null, types, null);
            return constructor.Invoke(parameters);
        }
    }
}