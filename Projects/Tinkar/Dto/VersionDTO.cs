using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// 
    /// </summary>
    public abstract record VersionDTO : ComponentDTO, IVersion
    {
        /// <summary>
        /// Gets the Stamp DTO.
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Gets the Stamp.
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        public VersionDTO(IPublicId publicId, StampDTO stamp) : base(publicId)
        {
            this.StampDTO = stamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionDTO"/> class.
        /// from input JSON stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        public VersionDTO(JObject jObj, IPublicId publicId) : base(jObj, publicId)
        {
            // base calls jObj.GetClass(jsonClassName);
            JObject jObjStamp = jObj.ReadToken<JObject>(ComponentFieldForJson.STAMP);
            this.StampDTO = new StampDTO(jObjStamp);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            VersionDTO other = otherObject as VersionDTO;
            if (other == null)
                return -1;

            Int32 cmp = base.CompareTo(other);
            if (cmp != 0)
                return cmp;
            cmp = this.Stamp.CompareTo(other.Stamp);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Marshal all fields to binary output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
            // note that PublicId is not written redundantly here,
            // they are written with the ConceptChronologyDTO...
            this.StampDTO.Marshal(output);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
            // note that PublicId is not written redundantly here,
            // they are written with the ConceptChronologyDTO...
            output.WritePropertyName(ComponentFieldForJson.STAMP);
            this.StampDTO.Marshal(output);
        }
    }
}
