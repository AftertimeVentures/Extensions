using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Provices extension methods for <see cref="Assembly"/>.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets types from the given <paramref name="assembly"/> that have an attribute of type <typeparamref name="TAnnotation"/>.
        /// </summary>
        /// <typeparam name="TAnnotation">Type of attribute to search for types with.</typeparam>
        /// <param name="assembly">Assembly to search for types.</param>
        /// <returns>Array of <see cref="AnnotatedType{TAnnotation}"/> representing selected types.</returns>
        public static AnnotatedType<TAnnotation>[] GetAnnotatedTypes<TAnnotation>( this Assembly assembly )
            where TAnnotation : Attribute
        {
            return assembly.GetTypes()
                .Select(t => new AnnotatedType<TAnnotation>(t))
                .Where(ati => ati.Annotation != null)
                .ToArray();
        }
    }
}
