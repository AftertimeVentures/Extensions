using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Moq;

using Aftertime.Extensions.Reflection.Samples;

namespace Aftertime.Extensions.Reflection
{
    public class ReflectExtensionsTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        public void GetAnnotatedMethods_Tests(int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
        {
            //  Prepare
            BindingFlags bindingFlags = BindingFlags.Public;
            BindingFlags? actualBindingFlags = default(BindingFlags?);

            MethodInfo[] methods = MockMethodInfoArray<AnnotationAttribute>(numberOfAnnotatedMethods, numberOfNonAnnotatedMethods);

            Mock<IReflect> reflectMock = new Mock<IReflect>();

            reflectMock.Setup(m => m.GetMethods(It.IsAny<BindingFlags>()))
                .Callback((BindingFlags bf) =>
                {
                    actualBindingFlags = bf;
                })
                .Returns(methods);

            //  Pre-validate
            Assert.Equal(numberOfAnnotatedMethods, methods.Count(mi => mi.GetCustomAttribute<AnnotationAttribute>() != null));
            Assert.Equal(numberOfNonAnnotatedMethods, methods.Count(mi => mi.GetCustomAttribute<AnnotationAttribute>() == null));

            //  Perform
            IEnumerable<AnnotatedMethodInfo<AnnotationAttribute>> annotatedMethods = reflectMock.Object
                .GetAnnotatedMethods<AnnotationAttribute>(bindingFlags);

            //  Post-validate
            Assert.NotNull(actualBindingFlags);
            Assert.Equal(bindingFlags, (BindingFlags)actualBindingFlags);
            Assert.Equal(numberOfAnnotatedMethods, annotatedMethods.Count());
        }

        private MethodInfo[] MockMethodInfoArray<TAttribute>(int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
            where TAttribute : Attribute
        {
            MethodInfo[] result = new MethodInfo[numberOfAnnotatedMethods + numberOfNonAnnotatedMethods];

            int pos = 0;

            while (--numberOfAnnotatedMethods >= 0)
            {
                result[pos++] = MockMethodInfo<TAttribute>(true);
            }

            while (--numberOfNonAnnotatedMethods >= 0)
            {
                result[pos++] = MockMethodInfo<TAttribute>(false);
            }

            return result;
        }

        private MethodInfo MockMethodInfo<TAttribute>(bool isAnnotated)
            where TAttribute : Attribute
        {
            Mock<MethodInfo> methodInfoMock = new Mock<MethodInfo>();

            if (isAnnotated)
            {
                Mock<TAttribute> attributeMock = new Mock<TAttribute>();

                methodInfoMock.Setup(m => m.GetCustomAttributes(typeof(TAttribute), It.IsAny<bool>()))
                    .Returns(new[] { attributeMock.Object });
            }

            return methodInfoMock.Object;
        }
    }
}
