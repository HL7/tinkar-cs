using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    public class TinkarJsonOutput : IDisposable
    {
        private JsonWriter writer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outStream"></param>
        public TinkarJsonOutput(Stream outStream, bool formatted = true)
        {
            this.writer = new JsonTextWriter(new StreamWriter(outStream));
            if (formatted == true)
                this.writer.Formatting = Formatting.Indented;
        }


        /// <summary>
        /// Write JSON start object tag.
        /// </summary>
        public void WriteStartObject() => this.writer.WriteStartObject();

        /// <summary>
        /// Write JSON end object tag.
        /// </summary>
        public void WriteEndObject() => this.writer.WriteEndObject();

        public void WritePropertyName(String propertyName) =>
            this.writer.WritePropertyName(propertyName);

        /// <summary>
        /// Write class property.
        /// </summary>
        public void WriteClass(String className)
        {
            this.writer.WritePropertyName(ComponentFieldForJson.CLASS);
            this.writer.WriteValue(className);
        }

        public void WriteMarshalableList(String propertyName,
            IEnumerable<IJsonMarshalable> items)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteStartArray();
            foreach (IJsonMarshalable item in items)
                item.Marshal(this);
            this.writer.WriteEndArray();
        }

        /// <summary>
        /// Write property that is array of guids
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="guids"></param>
        public void WriteUuids(String propertyName,
            IEnumerable<Guid> guids)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteStartArray();
            foreach (Guid guid in guids)
                this.writer.WriteValue(guid);
            this.writer.WriteEndArray();
        }

        /// <summary>
        /// Write property that is a date time
        /// </summary>
        public void WriteInstant(String propertyName,
            DateTime instant)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteValue(InstantUtil.Format(instant));
        }

        /// <summary>
        /// Write property that is a string
        /// </summary>
        public void WriteUTF(String propertyName,
            String value)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteValue(value);
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
