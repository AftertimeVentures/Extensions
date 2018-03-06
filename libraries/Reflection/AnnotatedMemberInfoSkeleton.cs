using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// A common base class for various kinds of Annotated* classes, i.e. <see cref="AnnotatedMemberInfo{TAnnotation}"/>,
    /// <see cref="AnnotatedMethodInfo{TAnnotation}"/>, <see cref="AnnotatedPropertyInfo{TAnnotation}"/>,
    /// <see cref="AnnotatedFieldInfo{TAnnotation}"/>, and <see cref="AnnotatedEventInfo{TAnnotation}"/>.
    /// Classes derived from <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}"/> represent annotated versions of respected
    /// member info types.
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public abstract class AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation : Attribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected AnnotatedMemberInfoSkeleton()
        {
            _lazyAnnotation = new Lazy<TAnnotation>(() => MemberInfo.GetCustomAttribute<TAnnotation>());
        }

        /// <summary>
        /// Attribute associated with the member represented by <see cref="MemberInfo"/>.
        /// </summary>
        public TAnnotation Annotation => _lazyAnnotation.Value;
        /// <summary>
        /// <see cref="MemberInfo"/> for the member that this Annotated* member info is related to.
        /// </summary>
        public abstract MemberInfo MemberInfo { get; }

        private readonly Lazy<TAnnotation> _lazyAnnotation;
    }
}
