using System;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Utilities.Reflection
{
    public sealed class AnnotatedTypeInfo<TAttribute>
        where TAttribute: Attribute
    {
        public TAttribute Attribute { get; set; }
        public Type Type { get; set; }
    }
}
