using System;
using System.Collections.Generic;
using System.Text;

namespace Aftertime.Utilities.Reflection.Samples
{
    internal static class SamplesWithMethodAnnotations
    {
        internal class InternalClass__with_public_and_private_instance_methods__none_annotated
        {
            public void PublicMethod()
            {

            }

            private void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_instance_methods__all_annotated
        {
            [Annotation]
            public void PublicMethod()
            {

            }

            [Annotation]
            private void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_instance_methods__public_annotated
        {
            [Annotation]
            public void PublicMethod()
            {

            }

            private void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_instance_methods__private_annotated
        {
            public void PublicMethod()
            {

            }

            [Annotation]
            private void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_static_methods__none_annotated
        {
            public static void PublicMethod()
            {

            }

            private static void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_static_methods__all_annotated
        {
            [Annotation]
            public static void PublicMethod()
            {

            }

            [Annotation]
            private static void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_static_methods__public_annotated
        {
            [Annotation]
            public static void PublicMethod()
            {

            }

            private static void PrivateMethod()
            {

            }
        }

        internal class InternalClass__with_public_and_private_static_methods__private_annotated
        {
            public static void PublicMethod()
            {

            }

            [Annotation]
            private static void PrivateMethod()
            {

            }
        }
    }
}
