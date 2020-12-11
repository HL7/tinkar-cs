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
        public static void GetClass(this JObject jObj,
            String expectedClassName)
        {
            String actualClassName = jObj[ComponentFieldForJson.CLASS]?.Value<String>();
            if (String.Compare(expectedClassName, actualClassName) != 0)
                throw new Exception($"Tinkar parse exception. Expected class '{expectedClassName}', received '{actualClassName}'");
        }

        public static T ReadToken<T>(this JObject jObj, String tokenName)
            where T : JToken
        {
            JToken token = jObj[tokenName] as JToken;
            if (token == null)
                throw new Exception($"Error parsing Tinkar json. Expected property '{tokenName}' not found");
            T retVal = token as T;
            if (retVal == null)
                throw new Exception($"Error parsing Tinkar json. Property '{tokenName}' is type {token.GetType().Name}, expected {typeof(T).Name}");
            return retVal;
        }

        public static DateTime ReadInstant(this JObject jObj, String propertyName) => 
            jObj.ReadToken<JValue>(propertyName).Value<DateTime>();

        public static String ReadString(this JObject jObj, String propertyName) =>
            jObj.ReadToken<JValue>(propertyName).Value<String>();

        public static IEnumerable<Guid> ReadUuids(this JObject jObj, String propertyName)
        {
            String[] guidStrings = jObj.ReadToken<JArray>(propertyName).Values<String>().ToArray();
            Guid[] retVal = new Guid[guidStrings.Length];
            for (Int32 i = 0; i < guidStrings.Length; i++)
                retVal[i] = new Guid(guidStrings[i]);
            return retVal;
        }

        public static IEnumerable<DefinitionForSemanticVersionDTO> ReadDefinitionForSemanticVersionList(
            this JObject jObj,
            IEnumerable<Guid> componentUuids)
        {
            List<DefinitionForSemanticVersionDTO> retVal = new List<DefinitionForSemanticVersionDTO>();
            JArray items = jObj.ReadToken<JArray>(ComponentFieldForJson.DEFINITION_VERSIONS);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(DefinitionForSemanticVersionDTO.Make(item, componentUuids));
            return retVal;
        }

        public static IEnumerable<FieldDefinitionDTO> ReadFieldDefinitionList(this JObject jObj)
        {
            List<FieldDefinitionDTO> retVal = new List<FieldDefinitionDTO>();
            JArray items = jObj.ReadToken<JArray>(ComponentFieldForJson.FIELD_DEFINITIONS);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(FieldDefinitionDTO.Make(item));
            return retVal;
        }

        public static IEnumerable<ConceptVersionDTO> ReadConceptVersionList(this JObject jObj, 
            IEnumerable<Guid> componentUuids)
        {
            List<ConceptVersionDTO> retVal = new List<ConceptVersionDTO>();

            JArray items = jObj.ReadToken<JArray>(ComponentFieldForJson.CONCEPT_VERSIONS);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(ConceptVersionDTO.Make(item, componentUuids));
            return retVal;
        }

        public static IEnumerable<SemanticVersionDTO> ReadSemanticVersionList(this JObject jObj,
            IEnumerable<Guid> componentUuids,
            IEnumerable<Guid> definitionForSemanticUuids,
            IEnumerable<Guid> referencedComponentUuids)
        {
            List<SemanticVersionDTO> retVal = new List<SemanticVersionDTO>();

            JArray items = jObj.ReadToken<JArray>(ComponentFieldForJson.VERSIONS);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(SemanticVersionDTO.Make(item, 
                    componentUuids, 
                    definitionForSemanticUuids,
                    referencedComponentUuids));
            return retVal;
        }
    }
}
