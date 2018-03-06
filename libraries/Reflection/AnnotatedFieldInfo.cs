using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
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
