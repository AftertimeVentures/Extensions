using System;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedTypeInfo<TAttribute>
        where TAttribute: Attribute
    {
        public TAttribute Attribute { get; set; }
        public Type Type { get; set; }
    }
}
