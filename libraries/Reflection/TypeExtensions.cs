using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Provides extension methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets all events on the given <paramref name="type"/> that match the specified 
        /// <paramref name="bindingFlags"/> and have an attribute of type <typeparamref name="TAnnotation"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">Attribute type to select events with.</typeparam>
        /// <param name="type">The type to retrieve events for.</param>
        /// <param name="bindingFlags">Binding flags to be used to select events.</param>
        /// <returns>Array of <see cref="AnnotatedEventInfo{TAnnotation}"/>. If no events matching the given 
        /// <paramref name="bindingFlags"/> and <typeparamref name="TAnnotation"/> are available through <paramref name="type"/>, 
        /// an empty array is returned. Both <see cref="AnnotatedEventInfo{TAnnotation}.EventInfo"/> and 
        /// <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> are guranteed
        /// to return non-null values for all items in the returned array.</returns>
        public static IEnumerable<AnnotatedEventInfo<TAnnotation>> GetAnnotatedEvents<TAnnotation>
            ( this Type type
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAnnotation : Attribute
        {
            return type.GetEvents(bindingFlags)
                .Select(ei => new AnnotatedEventInfo<TAnnotation>(ei))
                .Where(ami => ami.Annotation != null);
        }
    }
}
