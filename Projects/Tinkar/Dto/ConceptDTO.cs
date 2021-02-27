using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Concept record.
    /// </summary>
    public record ConceptDTO : BaseDTO<ConceptDTO>,
        IConcept,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int LocalMarshalVersion = 3;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JsonClassName = "ConceptDTO";

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        public ConceptDTO(IPublicId publicId)
        {
            this.PublicId = publicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public ConceptDTO(JObject jObj)
        {
            jObj.GetClass(JsonClassName);
            this.PublicId  = jObj.ReadPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        public ConceptDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
            this.PublicId = input.ReadPublicId();
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(ConceptDTO other) =>
            FieldCompare.ComparePublicIds(this.PublicId, other.PublicId);

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static ConceptDTO Make(TinkarInput input) =>
            new ConceptDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);;
            output.WriteUuids(this.PublicId);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>ConceptDTO record.</returns>
        public static ConceptDTO Make(JObject jObj) =>
            new ConceptDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(
                ComponentFieldForJson.COMPONENT_PUBLIC_ID,
                this.PublicId);
            output.WriteEndObject();
        }
    }
}
