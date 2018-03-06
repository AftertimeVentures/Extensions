using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Annotated counterpart for <see cref="MemberInfo"/>.
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public sealed class AnnotatedMemberInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation : Attribute
    {
        internal AnnotatedMemberInfo(MemberInfo memberInfo)
        {
            _memberInfo = memberInfo
                ?? throw new ArgumentNullException(nameof(memberInfo));
        }

        public override MemberInfo MemberInfo => MemberInfo;

        private readonly MemberInfo _memberInfo;
    }
}
