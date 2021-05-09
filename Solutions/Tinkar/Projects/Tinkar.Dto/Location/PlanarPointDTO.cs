using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar.Dto
{
    public record PlanarPointDTO : IPlanarPoint, IMarshalable, IJsonMarshalable
    {
        public const String JSONCLASSNAME = "PlanarPoint";

        public FieldDataType FieldDataType => FieldDataType.PlanarPoint;

        public Int32 X { get; init; }
        public Int32 Y { get; init; }

        public PlanarPointDTO(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }

        public Int32 CompareTo(Object other) => this.CompareTo(other as PlanarPointDTO);

        public Int32 CompareTo(IPlanarPoint other)
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

        public static PlanarPointDTO Make(JObject jsonObject)
        {
            Int32[] values = ((JArray)jsonObject["values"]).ToObject<int[]>();
            if (values.Length != 2)
                throw new Exception("Invalid json, expected 2 values");
            return new PlanarPointDTO(values[0], values[1]);
        }
    }
}
