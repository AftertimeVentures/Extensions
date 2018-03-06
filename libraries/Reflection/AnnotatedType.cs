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

        public TAnnotation Annotation => _lazyAnnotation.Value;
        public Type Type => _type;

        private readonly Lazy<TAnnotation> _lazyAnnotation;
        private readonly Type _type;
    }
}
