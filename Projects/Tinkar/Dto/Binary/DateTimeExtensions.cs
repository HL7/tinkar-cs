/*
 * Copyright 2020-2021 HL7.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// DateTime extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Java Instant starts at jan 1, 1970.
        /// </summary>
        private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Create a java value 'seconds since epochStart' from c# DateTime.
        /// </summary>
        /// <param name="dt">DateTime value.</param>
        /// <returns>Java integer seconds since 1970.</returns>
        public static Int64 EpochSecond(this DateTime dt)
        {
            TimeSpan ts = dt - epochStart;
            return (Int64)ts.TotalSeconds;
        }

        /// <summary>
        /// Create nano seconds from data time milliseconds.
        /// </summary>
        /// <param name="dt">Date time value.</param>
        /// <returns>nanoseconds part of date time.</returns>
        public static Int32 Nano(this DateTime dt)
        {
            TimeSpan ts = dt - epochStart;
            return ts.Milliseconds * 1000000;
        }

        /// <summary>
        /// Create C# date time from jave serialized instant fields.
        /// </summary>
        /// <param name="epoch">Seconds since (or before) 1970.</param>
        /// <param name="nanoSeconds">nanoseconds part of instant.</param>
        /// <returns>read DateTime value.</returns>
        public static DateTime FromInstant(
            Int64 epoch,
            Int32 nanoSeconds)
        {
            TimeSpan ts = new TimeSpan((epoch * TimeSpan.TicksPerSecond) + (nanoSeconds / 100));
            return DateTimeExtensions.epochStart + ts;
        }
    }
}
