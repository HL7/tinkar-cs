using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Binary
{
    /// <summary>
    /// Extension methods to IMarshalable interface
    /// </summary>
    public static class IMarshalableExtensions
    {
        public static TinkarByteArrayOutput Marshal(this IMarshalable me)
        {
            //$ Untested
            TinkarByteArrayOutput byteArrayOutput = TinkarByteArrayOutput.Make();
            me.Marshal(byteArrayOutput);
            return byteArrayOutput;
        }

        //static <T> T makeVersion(Class<T> objectClass, TinkarByteArrayOutput output, IEnumerable<Guid> componentUuids)
        //{
        //    //$ Untested
        //    return makeVersion(objectClass, output.getBytes(), componentUuids);
        //}

        //static <T> T makeVersion(Class<T> objectClass, byte[] input, IEnumerable<Guid> componentUuids)
        //{
        //    //$ Untested
        //    return makeVersion(objectClass, TinkarInput.Make(input), componentUuids);
        //}

        //static <T> T makeSemanticVersion(Class<T> objectClass, TinkarInput input, IEnumerable<Guid> componentUuids,
        //    IEnumerable<Guid> definitionForSemanticUuids, IEnumerable<Guid> referencedComponentUuids)
        //{
        //    //$ Untested
        //    try
        //    {
        //        return unmarshal(objectClass, SemanticVersionUnmarshaler.class, new Object[]
        //            {input, componentUuids, definitionForSemanticUuids, referencedComponentUuids});
        //    }
        //    catch (IllegalAccessException |

        //    IllegalArgumentException | InvocationTargetException ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}

        //static <T> T makeVersion(Class<T> objectClass, TinkarInput input, IEnumerable<Guid> componentUuids)
        //{
        //    //$ Untested
        //    try
        //    {
        //        return unmarshal(objectClass, VersionUnmarshaler.class, new Object[] {input, componentUuids});

        //    }
        //    catch (IllegalAccessException |

        //    IllegalArgumentException | InvocationTargetException ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}

        //static <T> T Make(Class<T> objectClass, TinkarInput input)
        //{
        //    //$ Untested
        //    try
        //    {
        //        return unmarshal(objectClass, Unmarshaler.class, new Object[] {input});

        //    }
        //    catch (IllegalAccessException |

        //    IllegalArgumentException | InvocationTargetException ex) {
        //        throw new MarshalExceptionUnchecked(ex);
        //    }
        //}

        //static <T> T Make(Class<T> objectClass, byte[] input)
        //{
        //    //$ Untested
        //    return Make(objectClass, TinkarInput.Make(input));
        //}

        //static <T> T Make(Class<T> objectClass, TinkarByteArrayOutput output)
        //{
        //    //$ Untested
        //    return Make(objectClass, TinkarInput.Make(output));
        //}

        //static <T> T unmarshal(Class<T> objectClass, Class<? extends Annotation> annotationClass,
        //Object[] parameters) throws IllegalAccessException, InvocationTargetException
        //{
        //    //$ Untested
        //    ArrayList<Method> unmarshalMethodList = getUnmarshalMethods(objectClass, annotationClass);
        //    if (unmarshalMethodList.isEmpty())
        //    {
        //        throw new MarshalExceptionUnchecked("No " + annotationClass.getSimpleName() +
        //                                            " method for class: " + objectClass);
        //    }
        //    else if (unmarshalMethodList.size() == 1)
        //    {
        //        Method unmarshalMethod = unmarshalMethodList.get(0);
        //        return (T) unmarshalMethod.invoke(null, parameters);
        //    }

        //    throw new MarshalExceptionUnchecked("More than one unmarshal method for class: " + objectClass
        //                                                                                     + " methods: " +
        //                                                                                     unmarshalMethodList);
        //}

        //static <T> ArrayList<Method>
        //    getUnmarshalMethods(Class<T> objectClass, Class<? extends Annotation> annotationClass)
        // {
        //    //$ Untested
        //    ArrayList<Method> unmarshalMethodList = new ArrayList<>();
        //    for (Method method:
        //    objectClass.getDeclaredMethods()) {
        //        for (Annotation annotation:
        //        method.getAnnotations()) {
        //            if (annotation.annotationType().equals(annotationClass))
        //            {
        //                if (Modifier.isStatic(method.getModifiers()))
        //                {
        //                    unmarshalMethodList.add(method);
        //                }
        //                else
        //                {
        //                    throw new MarshalExceptionUnchecked(annotationClass.getSimpleName() +
        //                                                        " method for class: " + objectClass
        //                                                        + " is not static: " + method);
        //                }
        //            }
        //        }
        //    }
        //    return unmarshalMethodList;
        //}
    }
}
