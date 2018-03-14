using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Annotated counterpart for <see cref="System.Type"/>.
    /// </summary>
    /// <typeparam name="TAnnotation">Type of annotation attribute.</typeparam>
    public sealed class AnnotatedType<TAnnotation>
        where TAnnotation: Attribute
    {
        internal AnnotatedType(Type type)
        {
            _type = type
                ?? throw new ArgumentNullException(nameof(type));

            _lazyAnnotation = new Lazy<TAnnotation>(() => _type.GetCustomAttribute<TAnnotation>());
        }

        /// <summary>
        /// Annnotation attribute on type.
        /// </summary>
        public TAnnotation Annotation => _lazyAnnotation.Value;
        
        /// <summary>
        /// Gets the non-annotated counterpart of this annotated type.
        /// </summary>
        public Type Type => _type;

        private readonly Lazy<TAnnotation> _lazyAnnotation;
        private readonly Type _type;
    }
}
