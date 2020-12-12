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
        /// Read a JSON JObject from stream and return it.
        /// </summary>
        /// <returns>Read JObject.</returns>
        public JObject ReadJsonObject() => JObject.Load(this.reader);
    }
}
