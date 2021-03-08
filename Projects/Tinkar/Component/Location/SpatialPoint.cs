﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public record SpatialPoint : IComparable<SpatialPoint>, IComparable, IMarshalable, IJsonMarshalable
    {
        public const String JSONCLASSNAME = "SpatialPoint";

        public FieldDataType FieldDataType => FieldDataType.SpatialPoint;

        public Int32 X { get; init; }
        public Int32 Y { get; init; }
        public Int32 Z { get; init; }

        public SpatialPoint(Int32 x, Int32 y, Int32 z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Int32 CompareTo(Object other) => CompareTo(other as SpatialPoint);

        public Int32 CompareTo(SpatialPoint other)
        {
            Int32 cmpVal = this.X.CompareTo(other.X);
            if (cmpVal != 0)
                return cmpVal;
            cmpVal = this.Y.CompareTo(other.Y);
            if (cmpVal != 0)
                return cmpVal;
            return this.Z.CompareTo(other.Z);
        }

        public void Marshal(TinkarOutput output)
        {
            output.WriteInt32(this.X);
            output.WriteInt32(this.Y);
            output.WriteInt32(this.Z);
        }

        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put("values", new Object[] { this.X, this.Y, this.Z });
            output.WriteEndObject();
        }

        public static SpatialPoint Make(JObject jsonObject)
        {
            throw new NotImplementedException();
        }
    }
}
