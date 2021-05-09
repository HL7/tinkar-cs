using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar.Dto
{
    /// <summary>
    /// Parses JSON into Tinkar records.
    /// </summary>
    public class TinkarJsonInput : IDisposable
    {
        private JsonReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinkarJsonInput"/> class.
        /// </summary>
        /// <param name="inStream">Stream of json data.</param>
        public TinkarJsonInput(Stream inStream)
        {
            this.reader = new JsonTextReader(new StreamReader(inStream));
        }

        /// <summary>
        /// Dispose method. Releases reference to stream.
        /// </summary>
        public void Dispose()
        {
            this.reader = null;
        }

        /// <summary>
        /// Read Tinkar items from file.
        /// </summary>
        /// <returns>Object[].</returns>
        public IEnumerable<ComponentDTO> GetComponents()
        {
            JObject jObject = this.ReadJsonObject();
            JArray items = (JArray) jObject["root"];

            foreach (JObject item in items)
            {
                String className = (String) item["class"];
                switch (className)
                {
                    case ConceptChronologyDTO.JSONCLASSNAME:
                        yield return ConceptChronologyDTO.Make(item);
                        break;
                    case PatternChronologyDTO.JSONCLASSNAME:
                        yield return PatternChronologyDTO.Make(item);
                        break;
                    case SemanticChronologyDTO.JSONCLASSNAME:
                        yield return SemanticChronologyDTO.Make(item);
                        break;

                    case ConceptDTO.JSONCLASSNAME:
                        yield return ConceptDTO.Make(item);
                        break;

                    case PatternDTO.JSONCLASSNAME:
                        yield return PatternDTO.Make(item);
                        break;

                    case SemanticDTO.JSONCLASSNAME:
                        yield return SemanticDTO.Make(item);
                        break;

                    default:
                        throw new NotImplementedException($"Tinkar class {className} not known");
                }
            }
        }

        /// <summary>
        /// Read a JSON JObject from stream and return it.
        /// </summary>
        /// <returns>Read JObject.</returns>
        public JObject ReadJsonObject() => JObject.Load(this.reader);
    }
}
