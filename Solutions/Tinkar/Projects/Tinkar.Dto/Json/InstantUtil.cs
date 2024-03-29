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

/// <summary>
/// Instant (DateTime) utilities.
/// </summary>
public static class InstantUtil
{
    /// <summary>
    /// Create string representation of DateTime.
    /// This format must be compatible
    /// with Tinkar-java instant format.
    /// </summary>
    /// <param name="dateTime">value to make string from.</param>
    /// <returns>Strign represetnation of instant.</returns>
    public static String Format(DateTime dateTime) => dateTime.ToString();

    /// <summary>
    /// Parse string representation of DateTime into date time.
    /// This date time format must be compatible
    /// with Tinkar-java instant format.
    /// </summary>
    /// <param name="possibleInstant">instant string.</param>
    /// <returns>DateTime instance.</returns>
    public static DateTime Parse(String possibleInstant)
    {
        try
        {
            DateTime instant = DateTime.Parse(possibleInstant);
            return instant;
        }
        catch
        {
            return DateTime.MinValue;
        }
    }
}
