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

        public void Dispose()
        {
            this.reader = null;
        }
    }
}
