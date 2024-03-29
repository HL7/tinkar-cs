using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar.Dto
{
    /// <summary>
    /// Concept record.
    /// </summary>
    public record ConceptDTO : ComponentDTO,
        IConcept,
        IDTO,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "ConceptDTO";

        /// <summary>
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public FieldDataType FieldDataType => FieldDataType.ConceptType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class.
        /// </summary>
        /// <param name = "uuids" > Component Public id(component ids).</param>
        public ConceptDTO(params Guid[] uuids) : this(new PublicId(uuids))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Component Public id(component ids).</param>
        public ConceptDTO(IPublicId componentPublicId) : base(componentPublicId)
        {
        }

        /// <summary>
        /// Returns zero of the two items are equivalent. This is not necessarily the
        /// same is same.
        /// </summary>
        /// <param name="otherObject"></param>
        /// <returns></returns>
        public override Boolean IsEquivalent(Object otherObject)
        {
            if (base.IsEquivalent(otherObject) == false)
                return false;

            ConceptDTO other = otherObject as ConceptDTO;
            if (other == null)
                return false;

            if (this == other)
                return true;

            return true;
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(Object otherObject)
        {
            ConceptDTO other = otherObject as ConceptDTO;
            if (other == null)
                return -1;
            return base.CompareTo(other);
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static ConceptDTO Make(TinkarInput input) =>
            new ConceptDTO(input.GetPublicId());

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>ConceptDTO record.</returns>
        public static ConceptDTO Make(JObject jObj) =>
            new ConceptDTO(jObj.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID));


        /// <summary>
        /// Mkae concept from a string of uuid's
        /// </summary>
        /// <param name="uuidListString"></param>
        /// <returns></returns>
        public static ConceptDTO Make(String uuidListString)
        {
            uuidListString = uuidListString
                .Replace("[", "")
                .Replace("]", "")
                .Replace(",", "")
                .Replace("\"", "");
            String[] uuidStrings = uuidListString.Split(" ");
            Guid[] uuids = new Guid[uuidStrings.Length];
            for (Int32 i = 0; i < uuidStrings.Length; i++)
                uuids[i] = new Guid(uuidStrings[i]);
            return new ConceptDTO(uuids);
        }

        /// <summary>
        /// Marshal all fields to output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarOutput output)
        {
            output.PutPublicId(this.PublicId);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.WriteEndObject();
        }
    }
}
