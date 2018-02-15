using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Xunit;

using Aftertime.Utilities.Reflection.Samples;

namespace Aftertime.Utilities.Reflection
{
    public class TypeExtensionsTests
    {
        [Theory]
        //
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__none_annotated)
        , BindingFlags.Default
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__all_annotated)
        , BindingFlags.Default
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__public_annotated)
        , BindingFlags.Default
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__private_annotated)
        , BindingFlags.Default
        , 0)]
        //
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__none_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__all_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 1)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__public_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 1)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__private_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 0)]
        //
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__none_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__all_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__public_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_instance_methods__private_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 0)]
        //
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__none_annotated)
        , BindingFlags.Default
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__all_annotated)
        , BindingFlags.Default
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__public_annotated)
        , BindingFlags.Default
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__private_annotated)
        , BindingFlags.Default
        , 0)]
        //
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__none_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__all_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__public_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__private_annotated)
        , BindingFlags.Instance | BindingFlags.Public
        , 0)]
        //
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__none_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 0)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__all_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 1)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__public_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 1)]
        [InlineData
        (typeof(SampleClasses.InternalClass__with_public_and_private_static_methods__private_annotated)
        , BindingFlags.Static | BindingFlags.Public
        , 0)]
        public void GetAnnotatedMethods__DefaultBindingFlags(Type type, BindingFlags bindingFlags, int expectedNumberOfMethods)
        {
            //  Prepare

            //  Pre-validate

            //  Perform
            IEnumerable<AnnotatedMethodInfo<AnnotationAttribute>> annotatedMethods = type
                .GetAnnotatedMethods<AnnotationAttribute>(bindingFlags);

            //  Post-validate
            Assert.Equal(expectedNumberOfMethods, annotatedMethods.Count());
        }
    }
}
