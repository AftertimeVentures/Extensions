using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Represents a common base class for various Annotated* classes, i.e. <see cref="AnnotatedMemberInfo{TAnnotation}"/>,
    /// <see cref="AnnotatedMethodInfo{TAttribute}{TAnnotation}"/>, <see cref="AnnotatedPropertyInfo{TAttribute}{TAnnotation}"/>,
    /// <see cref="AnnotatedFieldInfo{TAttribute}{TAnnotation}"/>, and <see cref="AnnotatedEventInfo{TAttribute}{TAnnotation}"/>.
    /// Classes derived from <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}"/> represent annotated versions of respected
    /// member infos.
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public abstract class AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation : Attribute
    {
        protected AnnotatedMemberInfoSkeleton()
        {
            _lazyAnnotation = new Lazy<TAnnotation>(() => MemberInfo.GetCustomAttribute<TAnnotation>());
        }

        /// <summary>
        /// Attribute associated with the member represented by <see cref="MemberInfo"/>.
        /// </summary>
        public TAnnotation Annotation => _lazyAnnotation.Value;
        /// <summary>
        /// MemberInfo of the member that this Annotated* type is associated with.
        /// </summary>
        public abstract MemberInfo MemberInfo { get; }

        private readonly Lazy<TAnnotation> _lazyAnnotation;
    }
}
