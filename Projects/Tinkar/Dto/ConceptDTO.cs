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
    public record ConceptDTO : ComponentDTO,
        IConcept,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "ConceptDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        public ConceptDTO(IPublicId publicId) : base(publicId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public ConceptDTO(JObject jObj) : base(jObj)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        protected ConceptDTO(TinkarInput input) : base(input)
        {
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
            new ConceptDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            base.MarshalFields(output);
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
        public override void MarshalFields(TinkarJsonOutput output)
        {
            base.MarshalFields(output);
        }
    }
}
