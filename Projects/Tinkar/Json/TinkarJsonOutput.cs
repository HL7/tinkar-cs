using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Serializes Tinkar records into JSON objects.
    /// </summary>
    public class TinkarJsonOutput : IDisposable
    {
        private JsonWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinkarJsonOutput"/> class.
        /// </summary>
        /// <param name="outStream">output stream.</param>
        /// <param name="formatted">if true, json is formatted to be easily readable.</param>
        public TinkarJsonOutput(Stream outStream, bool formatted = false)
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

        /// <summary>
        /// Created JSON child property with the indicated name.
        /// This does not assign a value to that property.
        /// </summary>
        /// <param name="propertyName">Name of property to create.</param>
        public void WritePropertyName(String propertyName) =>
            this.writer.WritePropertyName(propertyName);

        /// <summary>
        /// Write array of objects.
        /// </summary>
        /// <param name="propertyName">child property name.</param>
        /// <param name="fields">values to write.</param>
        public void WriteObjects(String propertyName, IEnumerable<Object> fields)
        {
            this.writer.WritePropertyName(propertyName);
            this.WriteObjects(fields);
        }

        /// <summary>
        /// Write field to output stream.
        /// </summary>
        /// <param name="field">value to write.</param>
        public void WriteField(Object field)
        {
            switch (field)
            {
                case Boolean item:
                    this.writer.WriteValue(item);
                    break;

                case byte[] item:
                    this.writer.WriteValue(item);
                    break;

                case Single item:
                    this.writer.WriteValue(item);
                    break;

                case Double item:
                    this.writer.WriteValue(item);
                    break;

                case Int32 item:
                    this.writer.WriteValue(item);
                    break;

                case Int64 item:
                    this.writer.WriteValue(item);
                    break;

                case String item:
                    this.writer.WriteValue(item);
                    break;

                case DateTime item:
                    this.writer.WriteValue(item);
                    break;

                case Object[] item:
                    this.WriteObjects(item);
                    break;

                case ConceptDTO item:
                    item.Marshal(this);
                    break;

                case ConceptChronologyDTO item:
                    item.Marshal(this);
                    break;

                case SemanticDTO item:
                    item.Marshal(this);
                    break;

                case SemanticChronologyDTO item:
                    item.Marshal(this);
                    break;

                case DefinitionForSemanticDTO item:
                    item.Marshal(this);
                    break;

                case DefinitionForSemanticChronologyDTO item:
                    item.Marshal(this);
                    break;

                //case DigraphDTO item:
                //    throw new NotImplementedException();

                default:
                    throw new NotSupportedException($"Can not serialize type {field.GetType().Name}");
            }
        }

        /// <summary>
        /// Write class property.
        /// </summary>
        /// <param name="className">name of class.</param>
        public void WriteClass(String className)
        {
            this.writer.WritePropertyName(ComponentFieldForJson.CLASS);
            this.writer.WriteValue(className);
        }

        /// <summary>
        /// Write series of IMarshable records to json.
        /// </summary>
        /// <param name="propertyName">Name of property containing serialized records.</param>
        /// <param name="items">item to serialize.</param>
        public void WriteMarshalableList(
            String propertyName,
            IEnumerable<IJsonMarshalable> items)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteStartArray();
            foreach (IJsonMarshalable item in items)
                item.Marshal(this);
            this.writer.WriteEndArray();
        }

        /// <summary>
        /// Write property that is array of guids.
        /// </summary>
        /// <param name="propertyName">Name of json property to write.</param>
        /// <param name="guids">Guids to write.</param>
        public void WriteUuids(
            String propertyName,
            IEnumerable<Guid> guids)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteStartArray();
            foreach (Guid guid in guids)
                this.writer.WriteValue(guid);
            this.writer.WriteEndArray();
        }

        /// <summary>
        /// Write property that is a date time.
        /// </summary>
        /// <param name="propertyName">Name of json property to write.</param>
        /// <param name="instant">value to write.</param>
        public void WriteInstant(
            String propertyName,
            DateTime instant)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteValue(InstantUtil.Format(instant));
        }

        /// <summary>
        /// Write property that is a string.
        /// </summary>
        /// <param name="propertyName">JSON property name that value will be written to.</param>
        /// <param name="value">Value.</param>
        public void WriteUTF(
            String propertyName,
            String value)
        {
            this.writer.WritePropertyName(propertyName);
            this.writer.WriteValue(value);
        }

        /// <summary>
        /// Dispose method. Flushes output stream and sets stream to null.
        /// If dispose is not called, then cached output may not be properly
        /// flushed to output stream.
        /// </summary>
        public void Dispose()
        {
            if (this.writer != null)
            {
                this.writer.Flush();
                this.writer = null;
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Write array of objects.
        /// </summary>
        /// <param name="fields">Fields to write.</param>
        private void WriteObjects(IEnumerable<Object> fields)
        {
            this.writer.WriteStartArray();
            foreach (Object field in fields)
                this.WriteField(field);
            this.writer.WriteEndArray();
        }
    }
}
