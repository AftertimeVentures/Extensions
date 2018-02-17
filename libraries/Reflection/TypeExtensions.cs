﻿using System;
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

        public static IEnumerable<AnnotatedPropertyInfo<TAttribute>> GetAnnotatedProperties<TAttribute>
            (this Type type
            , BindingFlags bindingFlags = BindingFlags.Default)
            where TAttribute : Attribute
        {
            return type.GetProperties(bindingFlags)
                .Select(pi => new AnnotatedPropertyInfo<TAttribute>()
                {
                    Attribute = pi.GetCustomAttribute<TAttribute>(),
                    PropertyInfo = pi,
                })
                .Where(ami => ami.Attribute != null);
        }
    }
}
