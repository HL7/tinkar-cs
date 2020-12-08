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
        private const int MarshalVersion = 1;

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
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to</param>
        /// <returns>-1, 0, or 1</returns>
        public override Int32 CompareTo(DefinitionForSemanticDTO other) =>
            FieldCompare.CompareGuids(this.ComponentUuids, other.ComponentUuids);

        //$@Override
        //public void jsonMarshal(Writer writer) {
        //    final JSONObject json = new JSONObject();
        //    json.put(ComponentFieldForJson.CLASS, this.getClass().getCanonicalName());
        //    json.put(ComponentFieldForJson.COMPONENT_UUIDS, componentUuids);
        //    json.writeJSONString(writer);
        //}

        //@JsonChronologyUnmarshaler
        //public static DefinitionForSemanticDTO Make(JSONObject jsonObject) {
        //    IEnumerable<Guid> componentUuids = jsonObject.asImmutableUuidList(ComponentFieldForJson.COMPONENT_UUIDS);
        //    return new DefinitionForSemanticDTO(componentUuids);
        //}

        /// <summary>
        /// Static method to Create DTO item from input stream.
        /// </summary>
        /// <param name="input">input data stream</param>
        /// <returns>new DTO item</returns>
        public static DefinitionForSemanticDTO Make(TinkarInput input)
        {
            CheckMarshalVersion(input, MarshalVersion);
            IEnumerable<Guid> componentUuids = input.ReadUuidArray();
            return new DefinitionForSemanticDTO(componentUuids);
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream</param>
        public void Marshal(TinkarOutput output)
        {
            WriteMarshalVersion(output, MarshalVersion);
            output.WriteUuidList(this.ComponentUuids);
        }
    }
}
