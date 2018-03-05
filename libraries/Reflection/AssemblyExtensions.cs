using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<AnnotatedTypeInfo<TAttribute>> GetAnnotatedTypes<TAttribute>( this Assembly assembly )
            where TAttribute : Attribute
        {
            return assembly.GetTypes()
                .Select(t => new AnnotatedTypeInfo<TAttribute>(t))
                .Where(ati => ati.Annotation != null);
        }
    }
}
