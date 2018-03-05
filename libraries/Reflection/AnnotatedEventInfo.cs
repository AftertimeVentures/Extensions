using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Represents an event coupled with an attribute of type <typeparamref name="TAttribute"/>,
    /// associated with it. If an event has no attributes of type <typeparamref name="TAttribute"/>,
    /// the <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> field is null.
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public sealed class AnnotatedEventInfo<TAttribute>
        : AnnotatedMemberInfoSkeleton<TAttribute>
        where TAttribute : Attribute
    {
        internal AnnotatedEventInfo(EventInfo eventInfo)
        {
            _eventInfo = eventInfo
                ?? throw new ArgumentNullException(nameof(eventInfo));
        }

        public EventInfo EventInfo { get; set; }

        public override MemberInfo MemberInfo => EventInfo;

        private readonly EventInfo _eventInfo;
    }
}
