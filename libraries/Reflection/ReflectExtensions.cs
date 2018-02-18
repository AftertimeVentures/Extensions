using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    public static class ReflectExtensions
    {
        public static AnnotatedMethodInfo<TAttribute>[] GetAnnotatedMethods<TAttribute>
            (this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default)
            where TAttribute : Attribute
        {
            return reflect.GetMethods(bindingFlags)
                .Select(mi => new AnnotatedMethodInfo<TAttribute>()
                {
                    Attribute = mi.GetCustomAttribute<TAttribute>(),
                    MethodInfo = mi,
                })
                .Where(ami => ami.Attribute != null)
                .ToArray();
        }

        public static AnnotatedPropertyInfo<TAttribute>[] GetAnnotatedProperties<TAttribute>
            (this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default)
            where TAttribute : Attribute
        {
            return reflect.GetProperties(bindingFlags)
                .Select(pi => new AnnotatedPropertyInfo<TAttribute>()
                {
                    Attribute = pi.GetCustomAttribute<TAttribute>(),
                    PropertyInfo = pi,
                })
                .Where(ami => ami.Attribute != null)
                .ToArray();
        }

        public static AnnotatedFieldInfo<TAttribute>[] GetAnnotatedFields<TAttribute>
            (this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default)
            where TAttribute : Attribute
        {
            return reflect.GetFields(bindingFlags)
                .Select(fi => new AnnotatedFieldInfo<TAttribute>()
                {
                    Attribute = fi.GetCustomAttribute<TAttribute>(),
                    FieldInfo = fi,
                })
                .Where(afi => afi.Attribute != null)
                .ToArray();
        }
    }
}
