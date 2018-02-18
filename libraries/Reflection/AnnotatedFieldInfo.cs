using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedFieldInfo<TAttribute>
        where TAttribute: Attribute
    {
        public TAttribute Attribute { get; set; }
        public FieldInfo FieldInfo { get; set; }
    }
}
