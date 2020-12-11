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
        const String TErr = "Error parsing Tinkar json.";

        public static void GetClass(this JObject jObj,
            String expectedClassName)
        {
            String actualClassName = jObj[ComponentFieldForJson.CLASS]?.Value<String>();
            if (String.Compare(expectedClassName, actualClassName) != 0)
                throw new Exception($"{TErr} Expected class '{expectedClassName}', received '{actualClassName}'");
        }

        public static T ReadToken<T>(this JObject jObj, String tokenName)
            where T : JToken
        {
            JToken token = jObj[tokenName] as JToken;
            if (token == null)
                throw new Exception($"{TErr} Expected property '{tokenName}' not found");
            T retVal = token as T;
            if (retVal == null)
                throw new Exception($"{TErr} Property '{tokenName}' is type {token.GetType().Name}, expected {typeof(T).Name}");
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

        static IJsonMarshalable ReadJsonMarshable(this JObject jObj)
        {
            String actualClassName = jObj[ComponentFieldForJson.CLASS]?.Value<String>();
            switch (actualClassName)
            {
                case null:
                    throw new Exception($"{TErr} Missing CLASS Declaration");
                case ConceptChronologyDTO.JsonClassName:
                    return ConceptChronologyDTO.Make(jObj);
                case ConceptDTO.JsonClassName:
                    return ConceptDTO.Make(jObj);
                case DefinitionForSemanticChronologyDTO.JsonClassName:
                    return DefinitionForSemanticChronologyDTO.Make(jObj);
                case DefinitionForSemanticDTO.JsonClassName:
                    return DefinitionForSemanticDTO.Make(jObj);
                case SemanticChronologyDTO.JsonClassName:
                    return SemanticChronologyDTO.Make(jObj);
                case SemanticDTO.JsonClassName:
                    return SemanticDTO.Make(jObj);
                default:
                    throw new NotImplementedException($"Class {actualClassName} not known");
            }
        }


        public static IEnumerable<Object> ReadObjects(this JObject jObj, String propertyName)
        {
            Object[] Parse(JArray jArray)
            {
                Object[] retVal = new object[jArray.Count];
                for (Int32 i = 0; i < jArray.Count; i++)
                {
                    JToken item = jArray[i];
                    switch (item)
                    {
                        case JArray value:
                            retVal[i] = Parse(value);
                            break;

                        case JValue value:
                            retVal[i] = value.Value<Object>();
                            break;

                        case JObject value:
                            retVal[i] = value.ReadJsonMarshable();
                            break;

                        default:
                            throw new Exception($"{TErr} Unexpected Fields type {item.GetType().Name}'");
                    }
                }
                return retVal;
            }

            JArray jArray = jObj.ReadToken<JArray>(propertyName);
            return Parse(jArray);
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
