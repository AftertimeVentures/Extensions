using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Xunit;

using Aftertime.Extensions.Reflection.Samples;

namespace Aftertime.Extensions.Reflection
{
    public class AssemblyExtensionsTests
    {
        [Fact]
        public void GetAnnotatedTypes_Tests()
        {
            //  Prepare

            //  Pre-validate

            //  Perform
            IEnumerable<AnnotatedType<AnnotationAttribute>> annotatedTypes = Assembly.GetExecutingAssembly()
                .GetAnnotatedTypes<AnnotationAttribute>();

            //  Post-validate
            Assert.Equal(1, annotatedTypes.Count());
        }
    }
}
