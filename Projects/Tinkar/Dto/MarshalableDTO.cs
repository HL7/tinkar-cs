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
        /// Name of this class in JSON serialization.
        /// This must be consistent with Java implementation.
        /// </summary>
        public virtual String JsonClassName { get; } = "MarshalableDTO";

        /// <summary>
        /// Constructor
        /// </summary>
        public MarshalableDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarshalableDTO"/> class.
        /// from input JSON stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        protected MarshalableDTO(JObject jObj)
        {
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
        }

        /// <summary>
        /// Marshal all fields to Json output stream.
        /// </summary>
        /// <param name="output">Json output stream.</param>
        public void Marshal(TinkarJsonOutput output)
        {
            output.WriteStartObject();
            output.WriteClass(this.JsonClassName);
            MarshalFields(output);
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