using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedMemberInfo<TAnnotation>
        where TAnnotation: Attribute
    {
        public TAnnotation Attribute { get; set; }
        public MemberInfo MemberInfo { get; set; }
    }
}
