using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedMethodInfo<TAttribute>
        where TAttribute : Attribute
    {
        public TAttribute Attribute { get; set; }
        public MethodInfo MethodInfo { get; set; }
    }
}
