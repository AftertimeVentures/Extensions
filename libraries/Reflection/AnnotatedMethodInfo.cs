using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Annotated counterpart for <see cref="System.Reflection.MemberInfo"/>
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public sealed class AnnotatedMethodInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation : Attribute
    {
        internal AnnotatedMethodInfo(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo
                ?? throw new ArgumentNullException(nameof(methodInfo));
        }

        /// <summary>
        /// Gets the non-annotated counterpart of this annotated method.
        /// </summary>
        public MethodInfo MethodInfo => _methodInfo;

        /// <summary>
        /// Gets the <see cref="AnnotatedMethodInfo{TAnnotation}.MethodInfo"/> object as <see cref="MemberInfo"/>. 
        /// Inherited from <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}"/>.
        /// </summary>
        public override MemberInfo MemberInfo => MethodInfo;

        private readonly MethodInfo _methodInfo;
    }
}
