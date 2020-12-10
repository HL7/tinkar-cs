using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    public static class JObjectExtensions
    {
        public static void ExpectPropertyClass(this JObject jObj,
            String expectedClassName)
        {
            String actualClassName = jObj[ComponentFieldForJson.CLASS]?.Value<String>();
            if (String.Compare(expectedClassName, actualClassName) != 0)
                throw new Exception($"Tinkar parse exception. Expected class '{expectedClassName}', received '{actualClassName}'");
        }

        static JToken Expect(this JObject jObj, String tokenName)
        {
            JToken p = jObj[tokenName] as JToken;
            if (p == null)
                throw new Exception($"Error parsing Tinkar json. Expected property '{tokenName}' not found");
            return p;
        }


        public static DateTime ReadInstant(this JObject jObj, String propertyName) => 
            jObj.Expect(propertyName).Value<DateTime>();


        public static IEnumerable<Guid> ReadUuids(this JObject jObj, String propertyName)
        {
            String[] guidStrings = jObj.Expect(propertyName).Values<String>().ToArray();
            Guid[] retVal = new Guid[guidStrings.Length];
            for (Int32 i = 0; i < guidStrings.Length; i++)
                retVal[i] = new Guid(guidStrings[i]);
            return retVal;
        }
    }
}
