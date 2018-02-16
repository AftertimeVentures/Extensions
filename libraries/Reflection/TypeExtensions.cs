using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    public static class TypeExtensions
    {
        public static IEnumerable<AnnotatedMethodInfo<TAttribute>> GetAnnotatedMethods<TAttribute>
            ( this Type type
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAttribute: Attribute
        {
            return type.GetMethods(bindingFlags)
                .Select(mi => new AnnotatedMethodInfo<TAttribute>()
                {
                    Attribute = mi.GetCustomAttribute<TAttribute>(),
                    MethodInfo = mi,
                })
                .Where(ami => ami.Attribute != null);
        }
    }
}
