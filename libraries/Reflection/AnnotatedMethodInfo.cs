using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedMethodInfo<TAttribute>
        : AnnotatedMemberInfoSkeleton<TAttribute>
        where TAttribute : Attribute
    {
        public MethodInfo MethodInfo { get; set; }

        protected override MemberInfo GetMemberInfo() => MethodInfo;
    }
}
