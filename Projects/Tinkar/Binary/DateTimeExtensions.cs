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
        public static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

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

        public static DateTime FromInstant(long epoch,
            Int32 nanoSeconds)
        {
            TimeSpan ts = new TimeSpan(epoch * TimeSpan.TicksPerSecond + nanoSeconds / 100);
            return DateTimeExtensions.epochStart + ts;
        }
    }
}
