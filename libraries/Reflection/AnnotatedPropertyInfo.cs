using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Annotated counterpart for <see cref="PropertyInfo"/>.
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public sealed class AnnotatedPropertyInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation: Attribute
    {
        internal AnnotatedPropertyInfo( PropertyInfo propertyInfo )
        {
            _propertyInfo = propertyInfo
                ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        /// <summary>
        /// Gets <see cref="PropertyInfo"/> for the member which this <see cref="AnnotatedPropertyInfo{TAnnotation}"/>
        /// is assoicated with.
        /// </summary>
        public PropertyInfo PropertyInfo => _propertyInfo;
        /// <summary>
        /// Provides access to the associated member via base <see cref="System.Reflection.MemberInfo"/> class. 
        /// Inherited from <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}"/>
        /// </summary>
        public override MemberInfo MemberInfo => PropertyInfo;

        private readonly PropertyInfo _propertyInfo;
    }
}
