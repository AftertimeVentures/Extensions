using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedPropertyInfo<TAttribute>
        : AnnotatedMemberInfo<TAttribute>
        where TAttribute: Attribute
    {
        public PropertyInfo PropertyInfo { get; set; }

        protected override MemberInfo GetMemberInfo() => PropertyInfo;
    }
}
