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
    public class TinkarJsonInput : IDisposable
    {
        private JsonReader reader;

        public TinkarJsonInput(Stream inStream)
        {
            this.reader = new JsonTextReader(new StreamReader(inStream));
        }

        public JObject ReadJsonObject() => JObject.Load(this.reader);

        public bool ReadProperty(out String propertyName)
        {
            propertyName = null;
            if (this.reader.Read() == false)
                throw new Exception($"Tinkar JSON parse error. Unexpected EOF");
            switch (this.reader.TokenType)
            {
                case JsonToken.EndObject:
                    return false;
                case JsonToken.PropertyName:
                    propertyName = (String) this.reader.Value;
                    return false;
                default:
                    throw new Exception($"Tinkar JSON parse error. Unexpected token {this.reader.TokenType} found reading object's properties");
            }
        }

        public DateTime ExpectPropertyInstant(String propertyName)
        {
            ExpectProperty(propertyName);
            String instantStr = this.ExpectStringToken();
            return InstantUtil.Parse(instantStr);
        }

        public IEnumerable<Guid> ExpectPropertyGuids(String propertyName)
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

        public String ValueAsString()
        {
            String value = this.reader.Value as String;
            if (value == null)
                throw new Exception($"Jason read error. Expected String, got {this.reader.Value.GetType().Name}");
            return value;
        }

        public void ExpectToken(JsonToken token)
        {
            if (this.reader.Read() == false)
                throw new Exception("Unexpected EOF reading from JSON stream");
            if (this.reader.TokenType != token)
                throw new Exception($"Unexpected token {reader.TokenType}, expecting {token}");
        }

        public void ExpectStartObject() => ExpectToken(JsonToken.StartObject);
        public void ExpectEndObject() => ExpectToken(JsonToken.EndObject);
        public void ExpectPropertyName() => ExpectToken(JsonToken.PropertyName);

        public void ExpectStringValue(String expectedValue)
        {
            String value = this.ValueAsString();
            if (String.Compare(expectedValue, value) != 0)
                throw new Exception($"Jason read error. Expected '{expectedValue}', got {value}");
        }

        public void ExpectProperty(String propertyName)
        {
            this.ExpectPropertyName();
            ExpectStringValue(propertyName);
        }

        public String ExpectStringToken()
        {
            ExpectToken(JsonToken.String);
            return this.ValueAsString();
        }

        public String ExpectPropertyString(String propertyName)
        {
            ExpectProperty(propertyName);
            return this.ExpectStringToken();
        }

        public void ReadStartObject() => ExpectStartObject();

        public void ReadEndObject() => ExpectEndObject();

        public IEnumerable<Guid> ReadUuids(String propertyName) =>
            ExpectPropertyGuids(propertyName);

        public DateTime ReadInstant(String propertyName) =>
            ExpectPropertyInstant(propertyName);

        public void ReadClass(String className)
        {
            String readClassName = this.ExpectPropertyString(ComponentFieldForJson.CLASS);
            if (String.Compare(readClassName, className) != 0)
                throw new Exception($"Expected class name {className}, received {readClassName}");
        }

        public void Dispose()
        {
            this.reader = null;
        }
    }
}
