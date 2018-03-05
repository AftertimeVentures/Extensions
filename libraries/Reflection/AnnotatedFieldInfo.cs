using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedFieldInfo<TAttribute>
        : AnnotatedMemberInfoSkeleton<TAttribute>
        where TAttribute: Attribute
    {
        public FieldInfo FieldInfo { get; set; }

        protected override MemberInfo GetMemberInfo() => FieldInfo;
    }
}
