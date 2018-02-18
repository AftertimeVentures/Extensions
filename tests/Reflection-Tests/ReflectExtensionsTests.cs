using System;
using System.Linq;
using System.Linq.Expressions;
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
        public void GetAnnotatedMethods_Baseline(int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
        {
            Run_GetAnnotated_X_BaselineTest(r => r.GetMethods(It.IsAny<BindingFlags>()), (r, bf) => r.GetAnnotatedMethods<AnnotationAttribute>(bf), numberOfAnnotatedMethods, numberOfNonAnnotatedMethods);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        public void GetAnnotatedProperties_Baseline(int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
        {
            Run_GetAnnotated_X_BaselineTest(r => r.GetProperties(It.IsAny<BindingFlags>()), (r, bf) => r.GetAnnotatedProperties<AnnotationAttribute>(bf), numberOfAnnotatedMethods, numberOfNonAnnotatedMethods);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        public void GetAnnotatedFields_Baseline(int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
        {
            Run_GetAnnotated_X_BaselineTest(r => r.GetFields(It.IsAny<BindingFlags>()), (r, bf) => r.GetAnnotatedFields<AnnotationAttribute>(bf), numberOfAnnotatedMethods, numberOfNonAnnotatedMethods);
        }

        private void Run_GetAnnotated_X_BaselineTest<T>(Expression<Func<IReflect, T[]>> memberSelectorExpr, Func<IReflect, BindingFlags, AnnotatedMemberInfo<AnnotationAttribute>[]> methodUnderTest, int numberOfAnnotatedMembers, int numberOfNonAnnotatedMembers)
            where T: MemberInfo
        {
            //  Prepare
            BindingFlags bindingFlags = BindingFlags.Public;
            BindingFlags? actualBindingFlags = default(BindingFlags?);

            T[] members = MockMemberInfoArray<T, AnnotationAttribute>(numberOfAnnotatedMembers, numberOfNonAnnotatedMembers);

            Mock<IReflect> reflectMock = new Mock<IReflect>();

            reflectMock.Setup(memberSelectorExpr)
                .Callback((BindingFlags bf) =>
                {
                    actualBindingFlags = bf;
                })
                .Returns(members);

            //  Pre-validate
            Assert.Equal(numberOfAnnotatedMembers, members.Count(mi => mi.GetCustomAttribute<AnnotationAttribute>() != null));
            Assert.Equal(numberOfNonAnnotatedMembers, members.Count(mi => mi.GetCustomAttribute<AnnotationAttribute>() == null));

            //  Perform
            IEnumerable<AnnotatedMemberInfo<AnnotationAttribute>> annotatedMembers = methodUnderTest(reflectMock.Object, bindingFlags);

            //  Post-validate
            Assert.NotNull(actualBindingFlags);
            Assert.Equal(bindingFlags, (BindingFlags)actualBindingFlags);
            Assert.Equal(numberOfAnnotatedMembers, annotatedMembers.Count());
            Assert.True(annotatedMembers.All(m => m.MemberInfo != null));
            Assert.True(annotatedMembers.All(m => m.Attribute != null));
        }

        private static Tuple<int, int>[] _getAnnotated_X_Tests_numberOfMethods = new Tuple<int, int>[]
        {
            new Tuple<int, int>(0, 0),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, 1),
            new Tuple<int, int>(2, 1),
            new Tuple<int, int>(1, 2),
            new Tuple<int, int>(2, 2),
        };

        private static IEnumerable<object[]> Get_GetAnnotated_X_Tests_Data()
        {
            foreach (Tuple<int, int> numbers in _getAnnotated_X_Tests_numberOfMethods)
            {
                yield return new object[] { typeof(MethodInfo), typeof(IReflect).GetMethod(nameof(IReflect.GetMethods)), numbers.Item1, numbers.Item2 };
            }
        }

        //[Theory]
        //[MemberData(nameof(Get_GetAnnotated_X_Tests_Data))]
        public void GetAnnotated_X_Tests(Type memberType, MethodInfo methodUnderTest, int numberOfAnnotatedMembers, int numberOfNonAnnotatedMembers)
        {
            //  Prepare
            BindingFlags bindingFlags = BindingFlags.Public;
            BindingFlags? actualBindingFlags = default(BindingFlags?);

            Expression expression = Expression.Call(methodUnderTest, Expression.Parameter(typeof(BindingFlags)));

            MethodInfo[] methods = MockMethodInfoArray<AnnotationAttribute>(numberOfAnnotatedMembers, numberOfNonAnnotatedMembers);

            Mock<IReflect> reflectMock = new Mock<IReflect>();

            Moq.Language.ICallback callbackSetup = typeof(Mock<IReflect>).GetMethod(nameof(Mock<IReflect>.Setup))
                .Invoke(reflectMock, new[] { expression }) as Moq.Language.ICallback;

            //reflectMock.Setup(m => m.GetMethods(BindingFlags.Default))
            //    .Callback((BindingFlags bf) =>
            //    {
            //        actualBindingFlags = bf;
            //    })
            //    .Returns(methods);

            //Moq.Language.I

            ////callbackSetup
            ////    .Callback<((BindingFlags bf) =>
            ////    {
            ////        actualBindingFlags = bf;
            ////    })
            ////    .Returns(methods);

            ////  Pre-validate
            //Assert.Equal(numberOfAnnotatedMembers, methods.Count(mi => mi.GetCustomAttribute<AnnotationAttribute>() != null));
            //Assert.Equal(numberOfNonAnnotatedMembers, methods.Count(mi => mi.GetCustomAttribute<AnnotationAttribute>() == null));

            ////  Perform
            //IEnumerable<AnnotatedMethodInfo<AnnotationAttribute>> annotatedMethods = reflectMock.Object
            //    .GetAnnotatedMethods<AnnotationAttribute>(bindingFlags);

            ////  Post-validate
            //Assert.NotNull(actualBindingFlags);
            //Assert.Equal(bindingFlags, (BindingFlags)actualBindingFlags);
            //Assert.Equal(numberOfAnnotatedMembers, annotatedMethods.Count());
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

        private T[] MockMemberInfoArray<T, TAttribute>(int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
            where T: MemberInfo
            where TAttribute : Attribute
        {
            T[] result = new T[numberOfAnnotatedMethods + numberOfNonAnnotatedMethods];

            int pos = 0;

            while (--numberOfAnnotatedMethods >= 0)
            {
                result[pos++] = MockMemberInfo<T, TAttribute>(true);
            }

            while (--numberOfNonAnnotatedMethods >= 0)
            {
                result[pos++] = MockMemberInfo<T, TAttribute>(false);
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

        private T MockMemberInfo<T, TAttribute>(bool isAnnotated)
            where T: MemberInfo
            where TAttribute : Attribute
        {
            Mock<T> methodInfoMock = new Mock<T>();

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
