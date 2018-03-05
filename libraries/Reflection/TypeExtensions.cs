using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aftertime.Extensions.Reflection
{
    public static class TypeExtensions
    {
        public static IEnumerable<AnnotatedEventInfo<TAttribute>> GetAnnotatedEvents<TAttribute>
            ( this Type type
            , BindingFlags bindingFlags = BindingFlags.Default )
            where TAttribute : Attribute
        {
            return type.GetEvents(bindingFlags)
                .Select(ei => new AnnotatedEventInfo<TAttribute>(ei))
                .Where(ami => ami.Annotation != null);
        }
    }
}
