using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public record PlanarPoint : IComparable, IComparable<PlanarPoint>, IMarshalable, IJsonMarshalable
    {
        public const String JSONCLASSNAME = "PlanarPoint";

        public FieldDataType FieldDataType => FieldDataType.PlanarPoint;

        public Int32 X { get; init; }
        public Int32 Y { get; init; }

        public PlanarPoint(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }

        public Int32 CompareTo(Object other) => CompareTo(other as PlanarPoint);

        public Int32 CompareTo(PlanarPoint other)
        {
            Int32 cmpVal = this.X.CompareTo(other.X);
            if (cmpVal != 0)
                return cmpVal;
            return this.Y.CompareTo(other.Y);
        }

        public void Marshal(TinkarOutput output)
        {
            output.WriteInt32(this.X);
            output.WriteInt32(this.Y);
        }

        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put("values", new Object[] { this.X, this.Y });
            output.WriteEndObject();
        }

        public static PlanarPoint Make(JObject jsonObject)
        {
            throw new NotImplementedException();
        }
    }
}
