using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Utilities.Reflection
{
    public sealed class AnnotatedMethodInfo<TAttribute>
        where TAttribute : Attribute
    {
        public TAttribute Attribute { get; set; }
        public MethodInfo MethodInfo { get; set; }
    }
}
