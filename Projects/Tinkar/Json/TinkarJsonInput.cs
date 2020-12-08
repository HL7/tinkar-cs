using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public class TinkarJsonInput : IDisposable
    {
        private JsonReader reader;

        public TinkarJsonInput(Stream inStream)
        {
            this.reader = new JsonTextReader(new StreamReader(inStream));
        }

        IEnumerable<Guid> ExpectPropertyGuids(String propertyName)
        {
            ExpectProperty(propertyName);
            List<Guid> retVal = new List<Guid>();
            ExpectToken(JsonToken.StartArray);
            bool done = false;
            while (done == false)
            {
                if (this.reader.Read() == false)
                    throw new Exception("Unexpected EOF reading from JSON stream");
                switch (this.reader.TokenType)
                {
                    case JsonToken.EndArray:
                        done = true;
                        break;

                    case JsonToken.String:
                    {
                        Guid g = new Guid(this.ValueAsString());
                        retVal.Add(g);
                    }
                        break;
                    default:
                        throw new Exception("Unexpected token {this.reader.TokenType} reading guid array");
                }
            }
            return retVal;
        }

        String ValueAsString()
        {
            String value = this.reader.Value as String;
            if (value == null)
                throw new Exception($"Jason read error. Expected String, got {this.reader.Value.GetType().Name}");
            return value;
        }

        void ExpectToken(JsonToken token)
        {
            if (this.reader.Read() == false)
                throw new Exception("Unexpected EOF reading from JSON stream");
            if (this.reader.TokenType != token)
                throw new Exception($"Unexpected token {reader.TokenType}, expecting {token}");
        }

        void ExpectStartObject() => ExpectToken(JsonToken.StartObject);
        void ExpectEndObject() => ExpectToken(JsonToken.EndObject);
        void ExpectPropertyName() => ExpectToken(JsonToken.PropertyName);

        void ExpectStringValue(String expectedValue)
        {
            String value = this.ValueAsString();
            if (String.Compare(expectedValue, value) != 0)
                throw new Exception($"Jason read error. Expected '{expectedValue}', got {value}");
        }

        void ExpectProperty(String propertyName)
        {
            this.ExpectPropertyName();
            ExpectStringValue(propertyName);
        }

        String ExpectStringToken()
        {
            ExpectToken(JsonToken.String);
            return this.ValueAsString();
        }

        String ExpectPropertyString(String propertyName)
        {
            ExpectProperty(propertyName);
            return this.ExpectStringToken();
        }

        /// <summary>
        /// Unmarshal Tinkar object from json stream.
        /// </summary>
        public IJsonMarshalable ReadClass()
        {
            this.ExpectStartObject();
            String className = this.ExpectPropertyString(ComponentFieldForJson.CLASS);
            switch (className)
            {
                case "ConceptDTO": return new ConceptDTO(this);
                default:
                    throw new Exception($"Error reading class. Unknown class '{className}'");
            }
        }

        public IEnumerable<Guid> ReadUuids(String propertyName) =>
            ExpectPropertyGuids(propertyName);

        public void Dispose()
        {
            this.reader = null;
        }
    }
}
