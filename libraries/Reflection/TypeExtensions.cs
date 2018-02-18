using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

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
                .Select(ei => new AnnotatedEventInfo<TAttribute>()
                {
                    Attribute = ei.GetCustomAttribute<TAttribute>(),
                    EventInfo = ei,
                })
                .Where(ami => ami.Attribute != null);
        }
    }
}
