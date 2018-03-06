using System;
using System.Linq;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Provides extension methods for <see cref="IReflect"/>.
    /// </summary>
    public static class ReflectExtensions
    {
        /// <summary>
        /// Gets all methods available from the given <see cref="IReflect"/> interface <paramref name="reflect"/> 
        /// that match the specified <paramref name="bindingFlags"/> and have an attribute of type <typeparamref name="TAnnotation"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">Attribute type to select methods with.</typeparam>
        /// <param name="reflect">The target object to retrieve methods from.</param>
        /// <param name="bindingFlags">Binding flags to be used to select methods.</param>
        /// <returns>Array of <see cref="AnnotatedMethodInfo{TAnnotation}"/>. If no methods matching the given 
        /// <paramref name="bindingFlags"/> and <typeparamref name="TAnnotation"/> are available from <paramref name="reflect"/>, 
        /// an empty array is returned. Both <see cref="AnnotatedMethodInfo{TAnnotation}.MethodInfo"/> and 
        /// <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> are guranteed
        /// to return non-null values for all items in the returned array.</returns>
        public static AnnotatedMethodInfo<TAnnotation>[] GetAnnotatedMethods<TAnnotation>
            ( this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAnnotation : Attribute
        {
            return reflect.GetMethods(bindingFlags)
                .Select(mi => new AnnotatedMethodInfo<TAnnotation>(mi))
                .Where(ami => ami.Annotation != null)
                .ToArray();
        }

        /// <summary>
        /// Gets all properties available from the given <see cref="IReflect"/> interface <paramref name="reflect"/> 
        /// that match the specified <paramref name="bindingFlags"/> and have an attribute of type <typeparamref name="TAnnotation"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">Attribute type to select properties with.</typeparam>
        /// <param name="reflect">The target object to retrieve properties from.</param>
        /// <param name="bindingFlags">Binding flags to be used to select properties.</param>
        /// <returns>Array of <see cref="AnnotatedPropertyInfo{TAnnotation}"/>. If no properties matching the given 
        /// <paramref name="bindingFlags"/> and <typeparamref name="TAnnotation"/> are available from <paramref name="reflect"/>, 
        /// an empty array is returned. Both <see cref="AnnotatedPropertyInfo{TAnnotation}.PropertyInfo"/> and 
        /// <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> are guranteed
        /// to return non-null values for all items in the returned array.</returns>
        public static AnnotatedPropertyInfo<TAnnotation>[] GetAnnotatedProperties<TAnnotation>
            ( this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAnnotation : Attribute
        {
            return reflect.GetProperties(bindingFlags)
                .Select(pi => new AnnotatedPropertyInfo<TAnnotation>(pi))
                .Where(api => api.Annotation != null)
                .ToArray();
        }

        /// <summary>
        /// Gets all fields available from the given <see cref="IReflect"/> interface <paramref name="reflect"/> 
        /// that match the specified <paramref name="bindingFlags"/> and have an attribute of type <typeparamref name="TAnnotation"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">Attribute type to select fields with.</typeparam>
        /// <param name="reflect">The target object to retrieve fields from.</param>
        /// <param name="bindingFlags">Binding flags to be used to select fields.</param>
        /// <returns>Array of <see cref="AnnotatedFieldInfo{TAnnotation}"/>. If no fields matching the given 
        /// <paramref name="bindingFlags"/> and <typeparamref name="TAnnotation"/> are available from <paramref name="reflect"/>, 
        /// an empty array is returned. Both <see cref="AnnotatedFieldInfo{TAnnotation}.FieldInfo"/> and 
        /// <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> are guranteed
        /// to return non-null values for all items in the returned array.</returns>
        public static AnnotatedFieldInfo<TAnnotation>[] GetAnnotatedFields<TAnnotation>
            ( this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAnnotation : Attribute
        {
            return reflect.GetFields(bindingFlags)
                .Select(fi => new AnnotatedFieldInfo<TAnnotation>(fi))
                .Where(afi => afi.Annotation != null)
                .ToArray();
        }

        /// <summary>
        /// Gets all members available from the given <see cref="IReflect"/> interface <paramref name="reflect"/> 
        /// that match the specified <paramref name="bindingFlags"/> and have an attribute of type <typeparamref name="TAnnotation"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">Attribute type to select members with.</typeparam>
        /// <param name="reflect">The target object to retrieve members from.</param>
        /// <param name="bindingFlags">Binding flags to be used to select members.</param>
        /// <returns>Array of <see cref="AnnotatedMemberInfo{TAnnotation}"/>. If no members matching the given 
        /// <paramref name="bindingFlags"/> and <typeparamref name="TAnnotation"/> are available from <paramref name="reflect"/>, 
        /// an empty array is returned. Both <see cref="AnnotatedMemberInfo{TAnnotation}.MemberInfo"/> and 
        /// <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> are guranteed
        /// to return non-null values for all items in the returned array.</returns>
        public static AnnotatedMemberInfo<TAnnotation>[] GetAnnotatedMembers<TAnnotation>
            ( this IReflect reflect
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAnnotation : Attribute
        {
            return reflect.GetMembers(bindingFlags)
                .Select(mi => new AnnotatedMemberInfo<TAnnotation>(mi))
                .Where(ami => ami.Annotation != null)
                .ToArray();
        }
    }
}
