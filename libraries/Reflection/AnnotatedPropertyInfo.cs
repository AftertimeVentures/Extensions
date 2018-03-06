using System;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedPropertyInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation: Attribute
    {
        public AnnotatedPropertyInfo( PropertyInfo propertyInfo )
        {
            _propertyInfo = propertyInfo
                ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public PropertyInfo PropertyInfo => _propertyInfo;

        public override MemberInfo MemberInfo => PropertyInfo;

        private readonly PropertyInfo _propertyInfo;
    }
}
