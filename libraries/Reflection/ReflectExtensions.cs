using System;
using System.Linq;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public static class ReflectExtensions
    {
        /// <summary>
        /// Gets all methods of <paramref name="reflect"/> with respect to given <paramref name="bindingFlags"/> 
        /// that have an attribute of type <typeparamref name="TAttribute"/> along with these attributes.
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to select methods with.</typeparam>
        /// <param name="reflect">The target object to retrieve methods for.</param>
        /// <param name="bindingFlags">Binding flags to be used to select methods from <paramref name="reflect"/>.</param>
        /// <returns>An array of <see cref="AnnotatedMethodInfo{TAttribute}"/> with <see cref="AnnotatedMethodInfo{TAttribute}.MethodInfo"/> 
        /// property containing about the method and <see cref="AnnotatedMemberInfo{TAnnotation}.Attribute"/> property containing an instance 
        /// of the attribute of type <typeparamref name="TAttribute"/> attached to it. If no methods matching the given 
        /// <paramref name="bindingFlags"/> and <typeparamref name="TAttribute"/> are found on <paramref name="reflect"/>, 
        /// an empty array is returned. Both <see cref="AnnotatedMethodInfo{TAttribute}.MethodInfo"/> and 
        /// <see cref="AnnotatedMemberInfo{TAnnotation}.Attribute"/> for all items in the result array are guranteed
        /// to be not null.</returns>
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
