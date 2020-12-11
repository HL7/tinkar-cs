﻿/*
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
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Java Instant starts at jan 1, 1970.
        /// </summary>
        private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static Int64 EpochSecond(this DateTime dt)
        {
            TimeSpan ts = dt - epochStart;
            return (Int64)ts.TotalSeconds;
        }

        public static Int32 Nano(this DateTime dt)
        {
            TimeSpan ts = dt - epochStart;
            return ts.Milliseconds * 1000000;
        }

        public static DateTime FromInstant(
            long epoch,
            Int32 nanoSeconds)
        {
            TimeSpan ts = new TimeSpan(epoch * TimeSpan.TicksPerSecond + nanoSeconds / 100);
            return DateTimeExtensions.epochStart + ts;
        }
    }
}
