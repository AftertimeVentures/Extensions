using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public abstract class AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation: Attribute
    {
        public TAnnotation Attribute { get; set; }

        protected abstract MemberInfo GetMemberInfo();
    }
}
