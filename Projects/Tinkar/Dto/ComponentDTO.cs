using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Base class for all Tinkar DTO classes.
    /// </summary>
    public record ComponentDTO : MarshalableDTO,
        IComponent,
        IComparable,
        IEquivalent
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName { get; } = "ComponentDTO";

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publicId">Public id for this item</param>
        public ComponentDTO(IPublicId publicId)
        {
            this.PublicId = publicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentDTO"/> class.
        /// from input JSON stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        public ComponentDTO(JObject jObj, IPublicId publicId) : this(publicId)
        {
            this.PublicId = publicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentDTO"/> class.
        /// from input JSON stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        protected ComponentDTO(JObject jObj) : base(jObj)
        {
            this.PublicId = jObj.ReadPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
        }

        /// <summary>
        /// Implementation of IEquivalent.IsEquivalent
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equivalence.</param>
        /// <returns>true if equal.</returns>
        public Boolean IsEquivalent(Object other) => this.CompareTo(other) == 0;

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public virtual Int32 CompareTo(Object otherObject)
        {
            ComponentDTO other = otherObject as ComponentDTO;
            if (other == null)
                return -1;
            Int32 cmp = FieldCompare.ComparePublicIds(this.PublicId, other.PublicId);
            if (cmp != 0)
                return cmp;
            return 0;
        }

        /// <summary>
        /// Marshal DTO item fields to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            output.WritePublicId(this.PublicId);
        }

        /// <summary>
        /// Marshal DTO item fields to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarJsonOutput output)
        {
            output.WritePublicId(
                ComponentFieldForJson.COMPONENT_PUBLIC_ID,
                this.PublicId);
        }
    }
}