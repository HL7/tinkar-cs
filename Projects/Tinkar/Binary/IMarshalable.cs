/*
 * Copyright 2020 kec.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;

namespace Tinkar
{
    /**
     *
     * Template for marshalable class implementations classes
     *

     private static final int MarshalVersion = 1;

     // Using a static method rather than a constructor eliminates the need for
     // a readResolve method, but allows the implementation to decide how
     // to handle special cases.

        public static StampDTO Make(TinkarInput input) {
            CheckMarshalVersion(input, MarshalVersion);
            ...
        }

        public void Marshal(TinkarOutput output) {
            WriteMarshalVersion(output, MarshalVersion);
            throw new UnsupportedOperationException();
        }
     *
     *
     */
    public interface IMarshalable
    {
        void Marshal(TinkarOutput output);

        //    default TinkarByteArrayOutput marshal()
        //{
        //    TinkarByteArrayOutput byteArrayOutput = TinkarByteArrayOutput.Make();
        //    marshal(byteArrayOutput);
        //    return byteArrayOutput;
        //}

        //static <T> T makeVersion(Class<T> objectClass, TinkarByteArrayOutput output, IEnumerable<Guid> componentUuids)
        //{
        //    return makeVersion(objectClass, output.getBytes(), componentUuids);
        //}

        //static <T> T makeVersion(Class<T> objectClass, byte[] input, IEnumerable<Guid> componentUuids)
        //{
        //    return makeVersion(objectClass, TinkarInput.Make(input), componentUuids);
        //}

        //static <T> T makeSemanticVersion(Class<T> objectClass, TinkarInput input, IEnumerable<Guid> componentUuids,
        //    IEnumerable<Guid> definitionForSemanticUuids, IEnumerable<Guid> referencedComponentUuids)
        //{
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
        //    return Make(objectClass, TinkarInput.Make(input));
        //}

        //static <T> T Make(Class<T> objectClass, TinkarByteArrayOutput output)
        //{
        //    return Make(objectClass, TinkarInput.Make(output));
        //}

        //static <T> T unmarshal(Class<T> objectClass, Class<? extends Annotation> annotationClass,
        //Object[] parameters) throws IllegalAccessException, InvocationTargetException {
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
        //    getUnmarshalMethods(Class<T> objectClass, Class<? extends Annotation> annotationClass) {
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