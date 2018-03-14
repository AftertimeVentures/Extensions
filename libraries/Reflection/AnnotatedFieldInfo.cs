using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    /// <summary>
    /// Annotated counterpart for <see cref="System.Reflection.FieldInfo"/>.
    /// </summary>
    /// <typeparam name="TAnnotation"></typeparam>
    public sealed class AnnotatedFieldInfo<TAnnotation>
        : AnnotatedMemberInfoSkeleton<TAnnotation>
        where TAnnotation: Attribute
    {
        internal AnnotatedFieldInfo(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo
                ?? throw new ArgumentNullException(nameof(fieldInfo));
        }

        /// <summary>
        /// Gets the non-annotated counterpart of this annotated field.
        /// </summary>
        public FieldInfo FieldInfo => _fieldInfo;

        /// <summary>
        /// Gets the <see cref="AnnotatedFieldInfo{TAnnotation}.FieldInfo"/> object as <see cref="MemberInfo"/>. 
        /// Inherited from <see cref="AnnotatedMemberInfoSkeleton{TAnnotation}"/>.
        /// </summary>
        public override MemberInfo MemberInfo => FieldInfo;

        private readonly FieldInfo _fieldInfo;
    }
}
