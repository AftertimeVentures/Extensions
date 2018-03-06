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

        public FieldInfo FieldInfo => _fieldInfo;

        public override MemberInfo MemberInfo => FieldInfo;

        private readonly FieldInfo _fieldInfo;
    }
}
