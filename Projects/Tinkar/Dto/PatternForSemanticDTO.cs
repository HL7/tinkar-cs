using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// PatternForSemantic record.
    /// </summary>
    public record PatternForSemanticDTO :
        ComponentDTO,
        IPatternForSemantic,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JSONCLASSNAME = "PatternForSemanticDTO";

        /// <summary>
        /// Gets JSON Name of class.
        /// </summary>
        public override String JsonClassName => JSONCLASSNAME;

        /// <summary>
        /// Unique ID for binary marshal of this item.
        /// </summary>
        public virtual FieldDataType FieldDataType => FieldDataType.PatternForSemanticType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternForSemanticDTO"/> class.
        /// </summary>
        /// <param name = "componentPublicId" > Public id(component ids).</param>
        public PatternForSemanticDTO(IPublicId componentPublicId) : base(componentPublicId)
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

            PatternForSemanticDTO other = otherObject as PatternForSemanticDTO;
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
            PatternForSemanticDTO other = otherObject as PatternForSemanticDTO;
            if (other == null)
                return -1;

            return base.CompareTo(other);
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream.</param>
        /// <returns>new DTO item.</returns>
        public static PatternForSemanticDTO Make(TinkarInput input) =>
            new PatternForSemanticDTO(input.GetPublicId());

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        /// <param name="jsonObject">JSON parent container to read from.</param>
        /// <returns>Deserialized PatternForSemanticDTO record.</returns>
        public static PatternForSemanticDTO Make(JObject jsonObject) =>
            new PatternForSemanticDTO(jsonObject.AsPublicId(ComponentFieldForJson.COMPONENT_PUBLIC_ID));

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void Marshal(TinkarOutput output) =>
            output.PutPublicId(this.PublicId);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public virtual void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JSONCLASSNAME);
            output.Put(ComponentFieldForJson.COMPONENT_PUBLIC_ID, this.PublicId);
            output.WriteEndObject();
        }
    }
}
