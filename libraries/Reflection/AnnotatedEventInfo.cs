using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Extensions.Reflection
{
    public sealed class AnnotatedEventInfo<TAttribute>
        where TAttribute: Attribute
    {
        public TAttribute Attribute { get; set; }
        public EventInfo EventInfo { get; set; }
    }
}
