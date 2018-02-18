using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedEventInfo<TAttribute>
        : AnnotatedMemberInfo<TAttribute>
        where TAttribute: Attribute
    {
        public EventInfo EventInfo { get; set; }

        protected override MemberInfo GetMemberInfo() => EventInfo;
    }
}
