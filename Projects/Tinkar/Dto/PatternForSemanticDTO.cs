using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// PatternForSemantic record.
    /// </summary>
    public record PatternForSemanticDTO :
        ComponentDTO<PatternForSemanticDTO>,
        IPatternForSemantic,
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
        public const String JSONCLASSNAME = "PatternForSemanticDTO";

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticDTO"/> class.
        /// </summary>
        /// <param name = "publicId" > Public id(component ids).</param>
        public PatternForSemanticDTO(IPublicId publicId) : base(publicId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticDTO"/> class
        /// from json stream.
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        public PatternForSemanticDTO(JObject jObj) : base(jObj)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticDTO"/> class
        /// from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        public PatternForSemanticDTO(TinkarInput input) : base(input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public override Int32 CompareTo(PatternForSemanticDTO other) => base.CompareTo(other);

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static PatternForSemanticDTO Make(TinkarInput input) =>
            new PatternForSemanticDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public override void MarshalFields(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);
            base.MarshalFields(output);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jObj">JSON parent container to read from.</param>
        /// <returns>Deserialized PatternForSemanticDTO record.</returns>
        public static PatternForSemanticDTO Make(JObject jObj) =>
            new PatternForSemanticDTO(jObj);

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
