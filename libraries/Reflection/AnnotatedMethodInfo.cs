using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Annotated counterpart for <see cref="System.Reflection.MemberInfo"/>
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public sealed class AnnotatedMethodInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation : Attribute
    {
        internal AnnotatedMethodInfo(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo
                ?? throw new ArgumentNullException(nameof(methodInfo));
        }

        public MethodInfo MethodInfo => _methodInfo;

        public override MemberInfo MemberInfo => MethodInfo;

        private readonly MethodInfo _methodInfo;
    }
}
