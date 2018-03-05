using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
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
