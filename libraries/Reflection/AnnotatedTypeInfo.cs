using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedTypeInfo<TAttribute>
        where TAttribute: Attribute
    {
        internal AnnotatedTypeInfo(Type type)
        {
            _type = type
                ?? throw new ArgumentNullException(nameof(type));

            _lazyAnnotation = new Lazy<TAttribute>(() => _type.GetCustomAttribute<TAttribute>());
        }

        public TAttribute Annotation => _lazyAnnotation.Value;
        public Type Type => _type;

        private readonly Lazy<TAttribute> _lazyAnnotation;
        private readonly Type _type;
    }
}
