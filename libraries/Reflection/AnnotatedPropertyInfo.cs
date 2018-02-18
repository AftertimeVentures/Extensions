using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedPropertyInfo<TAttribute>
        where TAttribute: Attribute
    {
        public TAttribute Attribute { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}
