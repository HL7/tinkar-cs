using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public class TinkarJsonOutput : IDisposable
    {
        private JsonWriter writer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outStream"></param>
        public TinkarJsonOutput(Stream outStream)
        {
            this.writer = new JsonTextWriter(new StreamWriter(outStream));
        }


        /// <summary>
        /// Marshal Tinkar object to json stream as Json object.
        /// </summary>
        public void WriteClass(IJsonMarshalable item)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(ComponentFieldForJson.CLASS);
            writer.WriteValue(item.GetType().Name);
            item.Marshal(this);
            writer.WriteEndObject();
        }

        /// <summary>
        /// Write property that is array of guids
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="guids"></param>
        public void WriteGuids(String propertyName,
            IEnumerable<Guid> guids)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteStartArray();
            foreach (Guid guid in guids)
                this.writer.WriteValue(guid);
            this.writer.WriteEndArray();
        }

        public void Dispose()
        {
            if (this.writer != null)
            {
                this.writer.Flush();
                this.writer = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
