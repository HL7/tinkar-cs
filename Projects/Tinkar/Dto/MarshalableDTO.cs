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
    public record MarshalableDTO
    {
        /// <summary>
        /// If true, create json sub class on json marshaling.
        /// </summary>
        protected virtual bool jsonClassFlag => true;

        /// <summary>
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public virtual String JsonClassName { get; } = "MarshalableDTO";

        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int LocalMarshalVersion = 3;

        /// <summary>
        /// Constructor
        /// </summary>
        public MarshalableDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptVersionDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">binary input stream.</param>
        protected MarshalableDTO(TinkarInput input)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarshalableDTO"/> class.
        /// from input JSON stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        protected MarshalableDTO(JObject jObj)
        {
            if (this.jsonClassFlag == true)
                jObj.GetClass(JsonClassName);
        }

        /// <summary>
        /// Marshal DTO item to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public void Marshal(TinkarOutput output)
        {
            MarshalFields(output);
        }

        /// <summary>
        /// Marshal DTO item fields to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void MarshalFields(TinkarOutput output)
        {
            output.CheckMarshalVersion(LocalMarshalVersion);
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            if (this.jsonClassFlag == true)
                output.WriteStartObject();
            MarshalFields(output);
            if (this.jsonClassFlag == true)
                output.WriteEndObject();
        }

        /// <summary>
        /// Marshal DTO item fields to output stream.
        /// </summary>
        /// <param name="output">output data stream.</param>
        public virtual void MarshalFields(TinkarJsonOutput output)
        {
        }
    }
}