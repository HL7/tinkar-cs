using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Common
{
#warning 'is this compatable with Java?'
    public static class GuidUtil
    {
        public static Int64 MostSignificantBits(this Guid me) => BitConverter.ToInt64(me.ToByteArray(), 0);
        public static Int64 LeastSignificantBits(this Guid me) => BitConverter.ToInt64(me.ToByteArray(), 8);

        public static Guid AsUuid(Int64 mostSignificantBits, Int64 leastSignificantBits)
        {
            byte[] guidData = new byte[16];
            Array.Copy(BitConverter.GetBytes(mostSignificantBits), 8, guidData, 0, 8);
            Array.Copy(BitConverter.GetBytes(leastSignificantBits), 0, guidData, 8, 8);
            return new Guid(guidData);
        }

        public static long[] AsArray(IEnumerable<Guid> uuids)
        {
            List<Guid> uuidList = uuids.ToList();
            uuidList.Sort();
            int size = uuidList.Count;
            long[] values = new long[size * 2];
            for (int i = 0; i < size; i++)
            {
                Guid uuid = uuidList[i];
                values[i * 2] = uuid.MostSignificantBits();
                values[(i * 2) + 1] = uuid.LeastSignificantBits();
            }
            return values;
        }

#warning 'Is this code correct?'
        public static bool AreEqual(IEnumerable<Guid> thisUuids, IEnumerable<Guid> otherUuids)
        {
            foreach (Guid otherUuid in otherUuids)
                if (thisUuids.Contains(otherUuid) == false)
                    return false;
            return true;
        }

        public static Guid[] ToArray(long[] array)
        {
            Guid[] uuidArray = new Guid[array.Length / 2];
            for (int i = 0; i < array.Length / 2; i++)
                uuidArray[i] = AsUuid(array[i * 2], array[(i * 2) + 1]);
            return uuidArray;
        }
    }
}
