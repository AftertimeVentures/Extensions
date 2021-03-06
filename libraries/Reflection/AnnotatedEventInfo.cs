﻿using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Represents an event coupled with an attribute of type <typeparamref name="TAnnotation"/>,
    /// associated with it. If an event has no attributes of type <typeparamref name="TAnnotation"/>,
    /// the <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}.Annotation"/> field is null.
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public sealed class AnnotatedEventInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation : Attribute
    {
        internal AnnotatedEventInfo(EventInfo eventInfo)
        {
            _eventInfo = eventInfo
                ?? throw new ArgumentNullException(nameof(eventInfo));
        }

        /// <summary>
        /// Gets the non-annotated counterpart of this annotated event.
        /// </summary>
        public EventInfo EventInfo { get; set; }

        /// <summary>
        /// Gets the <see cref="AnnotatedEventInfo{TAnnotation}.EventInfo"/> object as <see cref="MemberInfo"/>. 
        /// Inherited from <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}"/>.
        /// </summary>
        public override MemberInfo MemberInfo => EventInfo;

        private readonly EventInfo _eventInfo;
    }
}
