using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Tinkar
{
    public record DefinitionForSemanticDTO :
        BaseDTO<DefinitionForSemanticDTO>,
        IDefinitionForSemantic,
        IJsonMarshalable,
        IMarshalable
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int MarshalVersion = 1;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public const String JsonClassName = "DefinitionForSemanticDTO";


        /// <summary>
        /// Implementation of IIdentifiedThing.ComponentUuids.
        /// </summary>
        public IEnumerable<Guid> ComponentUuids { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentUuids">ComponentUuids</param>
        public DefinitionForSemanticDTO(IEnumerable<Guid> componentUuids)
        {
            this.ComponentUuids = componentUuids;
        }

        /// <summary>
        /// Create item from json stream
        /// </summary>
        public DefinitionForSemanticDTO(JObject jObj)
        {
            jObj.GetClass(JsonClassName);
            this.ComponentUuids = jObj.ReadUuids(ComponentFieldForJson.COMPONENT_UUIDS);
        }

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public DefinitionForSemanticDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(MarshalVersion);
            this.ComponentUuids = input.ReadUuids();
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(DefinitionForSemanticDTO other) =>
            FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static DefinitionForSemanticDTO Make(TinkarInput input) =>
            new DefinitionForSemanticDTO(input);

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            output.WriteMarshalVersion(MarshalVersion);
            output.WriteUuids(this.ComponentUuids);
        }

        /// <summary>
        /// Static method to Create DTO item from json .
        /// </summary>
        public static DefinitionForSemanticDTO Make(JObject jObj) =>
            new DefinitionForSemanticDTO(jObj);

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(JsonClassName);
            output.WriteUuids(ComponentFieldForJson.COMPONENT_UUIDS,
                this.ComponentUuids);
            output.WriteEndObject();
        }
    }
}
