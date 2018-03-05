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
        public MemberInfo MemberInfo { get; set; }

        protected override MemberInfo GetMemberInfo() => MemberInfo;
    }
}
