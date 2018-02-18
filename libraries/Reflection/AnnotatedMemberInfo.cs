using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public abstract class AnnotatedMemberInfo<TAnnotation>
        where TAnnotation: Attribute
    {
        public TAnnotation Attribute { get; set; }
        public MemberInfo MemberInfo => GetMemberInfo();

        protected abstract MemberInfo GetMemberInfo();
    }
}
