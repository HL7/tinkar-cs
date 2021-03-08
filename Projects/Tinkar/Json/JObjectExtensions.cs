using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Json extension methods.
    /// </summary>
    public static class JObjectExtensions
    {
        /// <summary>
        /// Common error message header.
        /// </summary>
        private const String TErr = "Error parsing Tinkar json.";

        /// <summary>
        /// Get class name as defined by child string property 'class'.
        /// Throw exception if class name does not match expected value.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="expectedClassName">expected class name.</param>
        public static void GetClass(
            this JObject jObj,
            String expectedClassName)
        {
            String actualClassName = jObj[ComponentFieldForJson.CLASS]?.Value<String>();
            if (String.Compare(expectedClassName, actualClassName) != 0)
                throw new Exception($"{TErr} Expected class '{expectedClassName}', received '{actualClassName}'");
        }

        /// <summary>
        /// Get child token of expected type and name.
        /// If child property does not exist an exception is thrown.
        /// If child item exists and
        /// is of incorrect type an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Expectfed type of json token.</typeparam>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="tokenName">child property name.</param>
        /// <returns>JSON token.</returns>
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

        /// <summary>
        /// Read DateTime value from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="propertyName">child property name.</param>
        /// <returns>Date time value.</returns>
        public static DateTime ReadInstant(this JObject jObj, String propertyName) =>
            jObj.ReadToken<JValue>(propertyName).Value<DateTime>();

        /// <summary>
        /// Read String value from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="propertyName">child property name.</param>
        /// <returns>String value.</returns>
        public static String ReadString(this JObject jObj, String propertyName) =>
            jObj.ReadToken<JValue>(propertyName).Value<String>();

        /// <summary>
        /// Read Guid values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="propertyName">child property name.</param>
        /// <returns>Guid values.</returns>
        public static IEnumerable<Guid> AsUuids(this JObject jObj, String propertyName)
        {
            String[] guidStrings = jObj.ReadToken<JArray>(propertyName).Values<String>().ToArray();
            Guid[] retVal = new Guid[guidStrings.Length];
            for (Int32 i = 0; i < guidStrings.Length; i++)
                retVal[i] = new Guid(guidStrings[i]);
            return retVal;
        }

        /// <summary>
        /// Read PublicId from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="propertyName">child property name.</param>
        /// <returns>Guid values.</returns>
        public static PublicId AsPublicId(this JObject jObj, String propertyName)
        {
            return new PublicId(jObj.AsUuids(propertyName).ToArray());
        }

        /// <summary>
        /// Read Object values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="propertyName">child property name.</param>
        /// <returns>Object values.</returns>
        public static IEnumerable<Object> AsObjects(this JObject jObj, String propertyName)
        {
            Object[] Parse(JArray jArray)
            {
                Object[] retVal = new Object[jArray.Count];
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

        /// <summary>
        /// Read PatternForSemanticVersionDTO values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <returns>Definition for semantic version values.</returns>
        public static IEnumerable<PatternForSemanticVersionDTO> ReadPatternForSemanticVersionList(
            this JObject jObj,
            IPublicId publicId)
        {
            List<PatternForSemanticVersionDTO> retVal = new List<PatternForSemanticVersionDTO>();
            JArray items = jObj.ReadToken<JArray>(ComponentFieldForJson.DEFINITION_VERSIONS);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(PatternForSemanticVersionDTO.Make(item, publicId));
            return retVal;
        }

        /// <summary>
        /// Read FieldDefinitionDTO values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name="fieldName">JSON child field name.</param>
        /// <returns>Field definition values.</returns>
        public static IEnumerable<FieldDefinitionDTO> AsFieldDefinitionList(this JObject jObj, String fieldName)
        {
            List<FieldDefinitionDTO> retVal = new List<FieldDefinitionDTO>();
            JArray items = jObj.ReadToken<JArray>(fieldName);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(FieldDefinitionDTO.Make(item));
            return retVal;
        }

        /// <summary>
        /// Read ConceptVersionDTO values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name = "fieldName" > JSON child field name</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <returns>Concept version values.</returns>
        public static IEnumerable<ConceptVersionDTO> AsConceptVersionList(
            this JObject jObj,
            String fieldName,
            IPublicId publicId)
        {
            List<ConceptVersionDTO> retVal = new List<ConceptVersionDTO>();

            JArray items = jObj.ReadToken<JArray>(fieldName);
            foreach (JObject item in items.Values<JObject>())
                retVal.Add(ConceptVersionDTO.Make(item, publicId));
            return retVal;
        }

        /// <summary>
        /// Read SemanticVersionDTO values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name = "fieldName" > Json child field name.</param>
        /// <param name="definitionForSemanticPublicId">Externally defined definition for semantic uuids.</param>
        /// <param name="referencedComponentPublicId">Externally defined referenced component uuids.</param>
        /// <returns>Semantic version values.</returns>
        public static IEnumerable<SemanticVersionDTO> ReadSemanticVersionList(
            this JObject jObj,
            String fieldName,
            IPublicId publicId,
            IPublicId definitionForSemanticPublicId,
            IPublicId referencedComponentPublicId)
        {
            List<SemanticVersionDTO> retVal = new List<SemanticVersionDTO>();

            JArray items = jObj.ReadToken<JArray>(fieldName);
            foreach (JObject item in items.Values<JObject>())
            {
                retVal.Add(SemanticVersionDTO.Make(
                    item,
                    publicId,
                    definitionForSemanticPublicId,
                    referencedComponentPublicId));
            }

            return retVal;
        }

        /// <summary>
        /// Read IJsonMarshalable values from the named child property.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <returns>IJsonMarshalable values.</returns>
        private static IJsonMarshalable ReadJsonMarshable(this JObject jObj)
        {
            String actualClassName = jObj[ComponentFieldForJson.CLASS]?.Value<String>();
            switch (actualClassName)
            {
                case null:
                    throw new Exception($"{TErr} Missing CLASS Declaration");
                case ConceptChronologyDTO.JSONCLASSNAME:
                    return ConceptChronologyDTO.Make(jObj);
                case ConceptDTO.JSONCLASSNAME:
                    return ConceptDTO.Make(jObj);
                case PatternForSemanticChronologyDTO.JSONCLASSNAME:
                    return PatternForSemanticChronologyDTO.Make(jObj);
                case PatternForSemanticDTO.JSONCLASSNAME:
                    return PatternForSemanticDTO.Make(jObj);
                case SemanticChronologyDTO.JSONCLASSNAME:
                    return SemanticChronologyDTO.Make(jObj);
                case SemanticDTO.JSONCLASSNAME:
                    return SemanticDTO.Make(jObj);
                case SpatialPoint.JSONCLASSNAME:
                    return SpatialPoint.Make(jObj);
                case PlanarPoint.JSONCLASSNAME:
                    return PlanarPoint.Make(jObj);
#warning ' add new classes'
                default:
                    throw new NotImplementedException($"Class {actualClassName} not known");
            }
        }
    }
}
