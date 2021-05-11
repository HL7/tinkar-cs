using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkar.Dto;
using Tinkar.ProtoBuf.CS;

namespace Tinkar.XUnitTests
{
    public static class DTOConvert
    {
        public static TinkarId ToTinkarId(this PBTinkarId id)
        {
            //# Tested
            return new TinkarId
            {
                id1 = (Int32)id.Id1,
                id2 = (Int32)id.Id2,
                id3 = (Int32)id.Id3,
                id4 = (Int32)id.Id4
            };
        }

        public static PublicId ToPublicId(this PBPublicId publicId)
        {
            //# Tested
            Guid[] tids = new Guid[publicId.Id.Count];
            for (Int32 i = 0; i < publicId.Id.Count; i++)
                tids[i] = publicId.Id[i].ToTinkarId().uuid;
            return new PublicId(tids);
        }

        public static StampDTO ToStamp(this PBStamp c)
        {
            //# Tested
            return new StampDTO(
                c.PublicId.ToPublicId(),
                c.Status.ToConcept().PublicId,
                c.Time.ToDateTime(),
                c.Author.ToConcept().PublicId,
                c.Module.ToConcept().PublicId,
                c.Path.ToConcept().PublicId);
        }

        static List<PublicId> ToPublicIdList(this PBPublicIdList value)
        {
            List<PublicId> retVal = new List<PublicId>(value.PublicIds.Count);
            foreach (PBPublicId pbPublicId in value.PublicIds)
                retVal.Add(pbPublicId.ToPublicId());
            return retVal;
        }

        static HashSet<IPublicId> ToPublicIdHash(this PBPublicIdList value)
        {
            HashSet<IPublicId> retVal = new HashSet<IPublicId>(value.PublicIds.Count);
            foreach (PBPublicId pbPublicId in value.PublicIds)
                retVal.Add(pbPublicId.ToPublicId());
            return retVal;
        }

        static ImmutableList<VertexDTO> ToVertexMap(this RepeatedField<PBVertex> value)
        {
            ImmutableList<VertexDTO>.Builder bldr = ImmutableList<VertexDTO>.Empty.ToBuilder();
            foreach (PBVertex item in value)
                bldr.Add(item.ToVertex());
            return bldr.ToImmutableList();
        }

        static ImmutableDictionary<Int32, Int32> ToImmutableDictionary(this RepeatedField<PBIntToIntMap> value)
        {
            ImmutableDictionary<Int32, Int32>.Builder bldr = ImmutableDictionary<Int32, Int32>.Empty.ToBuilder();
            foreach (PBIntToIntMap item in value)
                bldr.Add(item.Source, item.Target);
            return bldr.ToImmutableDictionary();
        }

        static ImmutableDictionary<Int32, ImmutableList<Int32>> ToImmutableDictionary(this RepeatedField<PBIntToMultipleIntMap> value)
        {
            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder bldr = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
            foreach (PBIntToMultipleIntMap item in value)
            {
                ImmutableList<Int32>.Builder l = ImmutableList<int>.Empty.ToBuilder();
                l.AddRange(item.Target);
                bldr.Add(item.Source, l.ToImmutableList());
            }
            return bldr.ToImmutableDictionary();
        }

        static DiTreeDTO ToDiTree(this PBDiTree value)
        {
            return new DiTreeDTO(
                value.Root.ToVertex(),
                value.PredecesorMap.ToImmutableDictionary(),
                value.VertexMap.ToVertexMap(),
                value.SuccessorMap.ToImmutableDictionary()
            );
        }

        static DiGraphDTO ToDiGraph(this PBDiGraph value)
        {
            return new DiGraphDTO(
                value.RootSequence.ToImmutableList(),
                value.PredecesorMap.ToImmutableDictionary(),
                value.VertexMap.ToVertexMap(),
                value.SuccessorMap.ToImmutableDictionary()
            );
        }

        static GraphDTO ToGraph(this PBGraph value)
        {
            return new GraphDTO(
                value.VertexMap.ToVertexMap(),
                value.SuccessorMap.ToImmutableDictionary()
            );
        }

        static VertexDTO ToVertex(this PBVertex value)
        {
            return new VertexDTO()
            throw new NotImplementedException();
        }

        public static IEnumerable<Object> ToObjects(this IEnumerable<PBField> c)
        {
            //# Tested
            foreach (PBField f in c)
            {
                switch (f.ValueCase)
                {
                    case PBField.ValueOneofCase.StringValue:
                        yield return f.StringValue;
                        break;
                    case PBField.ValueOneofCase.BoolValue:
                        yield return f.BoolValue;
                        break;
                    case PBField.ValueOneofCase.IntValue:
                        yield return f.IntValue;
                        break;
                    case PBField.ValueOneofCase.FloatValue:
                        yield return f.FloatValue;
                        break;
                    case PBField.ValueOneofCase.BytesValue:
                        yield return f.BytesValue;
                        break;
                    case PBField.ValueOneofCase.TimeValue:
                        yield return f.TimeValue.ToDateTime();
                        break;
                    case PBField.ValueOneofCase.ConceptValue:
                        yield return f.ConceptValue.ToConcept();
                        break;
                    case PBField.ValueOneofCase.PublicIdListValue:
                        yield return f.PublicIdListValue.ToPublicIdList();
                        break;
                    case PBField.ValueOneofCase.PublicIdHashValue:
                        yield return f.PublicIdHashValue.ToPublicIdHash();
                        break;

                    case PBField.ValueOneofCase.DiTreeValue:
                        yield return f.DiTreeValue.ToDiTree();
                        break;

                    case PBField.ValueOneofCase.DiGraphValue:
                        yield return f.DiGraphValue.ToDiGraph();
                        break;

                    case PBField.ValueOneofCase.VertexValue:
                        yield return f.VertexValue.ToVertex();
                        break;

                    case PBField.ValueOneofCase.GraphValue:
                        yield return f.GraphValue.ToGraph();
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        #region Pattern
        public static PatternDTO ToPattern(this PBPattern c)
        {
            //# Not Tested
            return new PatternDTO(c.PublicId.ToPublicId());
        }

        static FieldDefinitionDTO ToFieldDefinition(this PBFieldDefinition field)
        {
            return new FieldDefinitionDTO(
                field.DataType.ToPublicId(),
                field.Purpose.ToPublicId(),
                field.Meaning.ToPublicId()
            );
        }

        static IEnumerable<FieldDefinitionDTO> ToFieldDefinitions(this RepeatedField<PBFieldDefinition> fields)
        {
            foreach (PBFieldDefinition field in fields)
                yield return field.ToFieldDefinition();
        }

        public static PatternVersionDTO ToPatternVersion(this PBPatternVersion c)
        {
            //# Tested
            return new PatternVersionDTO(
                c.PublicId.ToPublicId(),
                c.Stamp.ToStamp(),
                c.ReferencedComponentPurpose.ToPublicId(),
                c.ReferencedComponentMeaning.ToPublicId(),
                c.FieldDefinitions.ToFieldDefinitions().ToImmutableArray());
        }

        public static PatternChronologyDTO ToPatternChronology(this PBPatternChronology c)
        {
            //# Tested
            PatternVersionDTO[] versions = new PatternVersionDTO[c.Versions.Count];
            for (Int32 i = 0; i < c.Versions.Count; i++)
                versions[i] = c.Versions[i].ToPatternVersion();

            PatternChronologyDTO retVal = new PatternChronologyDTO(
                c.PublicId.ToPublicId(),
                versions.ToImmutableArray());
            return retVal;
        }
        #endregion
        
        #region Semantic
        public static SemanticDTO ToSemantic(this PBSemantic c)
        {
            //# Not Tested
            return new SemanticDTO(
                c.PublicId.ToPublicId(),
                c.PatternForSemantic.ToPublicId(),
                c.ReferencedComponent.ToPublicId());
        }

        public static SemanticVersionDTO ToSemanticVersion(this PBSemanticVersion c)
        {
            //# Tested
            return new SemanticVersionDTO(
                c.PublicId.ToPublicId(),
                c.PatternForSemantic.ToPublicId(),
                c.ReferencedComponent.ToPublicId(),
                c.Stamp.ToStamp(),
                c.FieldValues.ToObjects().ToImmutableArray()
                );
        }

        public static SemanticChronologyDTO ToSemanticChronology(this PBSemanticChronology c)
        {
            //# Tested
            SemanticVersionDTO[] versions = new SemanticVersionDTO[c.Versions.Count];
            for (Int32 i = 0; i < c.Versions.Count; i++)
                versions[i] = c.Versions[i].ToSemanticVersion();

            SemanticChronologyDTO retVal = new SemanticChronologyDTO(
                c.PublicId.ToPublicId(),
                c.PatternForSemantic.ToPublicId(),
                c.ReferencedComponent.ToPublicId(),
                versions.ToImmutableArray());
            return retVal;
        }
        #endregion

        #region Concept
        public static ConceptDTO ToConcept(this PBConcept c)
        {
            //# Tested
            return new ConceptDTO(c.PublicId.ToPublicId());
        }

        public static ConceptVersionDTO ToConceptVersion(this PBConceptVersion c)
        {
            //# Not Tested
            return new ConceptVersionDTO(c.PublicId.ToPublicId(), c.Stamp.ToStamp());
        }

        public static ConceptChronologyDTO ToConceptChronology(this PBConceptChronology c)
        {
            //# Not Tested
            ConceptVersionDTO[] versions = new ConceptVersionDTO[c.ConceptVersions.Count];
            for (Int32 i = 0; i < c.ConceptVersions.Count; i++)
                versions[i] = c.ConceptVersions[i].ToConceptVersion();
            return new ConceptChronologyDTO(c.PublicId.ToPublicId(), versions.ToImmutableArray());
        }
        #endregion

        public static IComponent Convert(Object c)
        {
            switch (c)
            {
                case PBConcept item:
                    return item.ToConcept();
                case PBConceptVersion item:
                    return item.ToConceptVersion();
                case PBConceptChronology item:
                    return item.ToConceptChronology();

                case PBPattern item:
                    return item.ToPattern();
                case PBPatternVersion item:
                    return item.ToPatternVersion();
                case PBPatternChronology item:
                    return item.ToPatternChronology();

                case PBSemantic item:
                    return item.ToSemantic();
                case PBSemanticVersion item:
                    return item.ToSemanticVersion();
                case PBSemanticChronology item:
                    return item.ToSemanticChronology();

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
