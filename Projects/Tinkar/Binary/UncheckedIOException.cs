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
    public class UncheckedIOException : Exception
    {

        public UncheckedIOException(Exception ex) : base(ex.Message, ex)
        {
        }

        public UncheckedIOException(String message) : base(message)
        {
        }

        //$public UncheckedIOException(Throwable cause)
        //{
        //    super(cause);
        //}
    }
}