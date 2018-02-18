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
        [MemberData(nameof(_getData))]
        public void GetAnnotatedFields_Baseline_Experimental(Type type, Expression memberSelectorExpr, Expression methodUnderTestDelegate,int numberOfAnnotatedMethods, int numberOfNonAnnotatedMethods)
        {
            GetType().GetMethod(nameof(Run_GetAnnotated_X_BaselineTest), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { memberSelectorExpr, methodUnderTestDelegate, numberOfAnnotatedMethods, numberOfNonAnnotatedMethods });
        }

        public static IEnumerable<Tuple<int, int>> _getNumberOfMembersPairs()
        {
            yield return new Tuple<int, int>(0, 0);
            yield return new Tuple<int, int>(1, 0);
            yield return new Tuple<int, int>(0, 1);
            yield return new Tuple<int, int>(1, 1);
            yield return new Tuple<int, int>(2, 1);
            yield return new Tuple<int, int>(1, 2);
            yield return new Tuple<int, int>(2, 2);
        }

        public static IEnumerable<Tuple<Type, Expression, Expression>> _getTestExpressionTuples()
        {
            ParameterExpression parameterReflect = Expression.Parameter(typeof(IReflect));
            ParameterExpression parameterBindingFlags = Expression.Parameter(typeof(BindingFlags));

            yield return new Tuple<Type, Expression, Expression>(
                typeof(FieldInfo),
                Expression.Lambda(typeof(Func<IReflect, FieldInfo[]>), Expression.Call(Expression.Parameter(typeof(IReflect)), typeof(IReflect).GetMethod(nameof(IReflect.GetFields)), Expression.Call(typeof(It).GetMethod(nameof(It.IsAny)).MakeGenericMethod(typeof(BindingFlags)))), Expression.Parameter(typeof(IReflect))),
                Expression.Lambda(typeof(Func<IReflect, BindingFlags, AnnotatedMemberInfo<AnnotationAttribute>[]>), Expression.Call(typeof(ReflectExtensions).GetMethod(nameof(ReflectExtensions.GetAnnotatedFields)).MakeGenericMethod(typeof(AnnotationAttribute)), parameterReflect, parameterBindingFlags), parameterReflect, parameterBindingFlags)
            );

            yield return new Tuple<Type, Expression, Expression>(
                typeof(MethodInfo),
                Expression.Lambda(typeof(Func<IReflect, MethodInfo[]>), Expression.Call(Expression.Parameter(typeof(IReflect)), typeof(IReflect).GetMethod(nameof(IReflect.GetMethods)), Expression.Call(typeof(It).GetMethod(nameof(It.IsAny)).MakeGenericMethod(typeof(BindingFlags)))), Expression.Parameter(typeof(IReflect))),
                Expression.Lambda(typeof(Func<IReflect, BindingFlags, AnnotatedMemberInfo<AnnotationAttribute>[]>), Expression.Call(typeof(ReflectExtensions).GetMethod(nameof(ReflectExtensions.GetAnnotatedMethods)).MakeGenericMethod(typeof(AnnotationAttribute)), parameterReflect, parameterBindingFlags), parameterReflect, parameterBindingFlags)
            );

            yield return new Tuple<Type, Expression, Expression>(
                typeof(PropertyInfo),
                Expression.Lambda(typeof(Func<IReflect, PropertyInfo[]>), Expression.Call(Expression.Parameter(typeof(IReflect)), typeof(IReflect).GetMethod(nameof(IReflect.GetProperties)), Expression.Call(typeof(It).GetMethod(nameof(It.IsAny)).MakeGenericMethod(typeof(BindingFlags)))), Expression.Parameter(typeof(IReflect))),
                Expression.Lambda(typeof(Func<IReflect, BindingFlags, AnnotatedMemberInfo<AnnotationAttribute>[]>), Expression.Call(typeof(ReflectExtensions).GetMethod(nameof(ReflectExtensions.GetAnnotatedProperties)).MakeGenericMethod(typeof(AnnotationAttribute)), parameterReflect, parameterBindingFlags), parameterReflect, parameterBindingFlags)
            );
        }

        public static IEnumerable<object[]> _getData()
        {
            foreach (Tuple<Type, Expression, Expression> expressions in _getTestExpressionTuples())
            {
                ParameterExpression parameterReflect = Expression.Parameter(typeof(IReflect));
                ParameterExpression parameterBindingFlags = Expression.Parameter(typeof(BindingFlags));

                foreach (Tuple<int, int> numbers in _getNumberOfMembersPairs())
                {
                    yield return new object[] {
                        expressions.Item1,
                        expressions.Item2,
                        expressions.Item3,
                        1,
                        1,
                    };
                }
            }
        }

        private void Run_GetAnnotated_X_BaselineTest<T>(Expression<Func<IReflect, T[]>> memberSelectorExpr, Expression<Func<IReflect, BindingFlags, AnnotatedMemberInfo<AnnotationAttribute>[]>> methodUnderTest, int numberOfAnnotatedMembers, int numberOfNonAnnotatedMembers)
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
            IEnumerable<AnnotatedMemberInfo<AnnotationAttribute>> annotatedMembers = methodUnderTest.Compile().Invoke(reflectMock.Object, bindingFlags);

            //  Post-validate
            Assert.NotNull(actualBindingFlags);
            Assert.Equal(bindingFlags, (BindingFlags)actualBindingFlags);
            Assert.Equal(numberOfAnnotatedMembers, annotatedMembers.Count());
            Assert.True(annotatedMembers.All(m => m.MemberInfo != null));
            Assert.True(annotatedMembers.All(m => m.Attribute != null));
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
