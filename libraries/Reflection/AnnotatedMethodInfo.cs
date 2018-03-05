using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedMethodInfo<TAttribute>
        : AnnotatedMemberInfoSkeleton<TAttribute>
        where TAttribute : Attribute
    {
        public AnnotatedMethodInfo(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo
                ?? throw new ArgumentNullException(nameof(methodInfo));
        }

        public MethodInfo MethodInfo => _methodInfo;

        public override MemberInfo MemberInfo => MethodInfo;

        private readonly MethodInfo _methodInfo;
    }
}
