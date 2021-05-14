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
    public static class ConvertPBToDTO
    {
        public static TinkarId ToTinkarId(this PBTinkarId id)
        {
            //# Tested
            return new TinkarId
            {
                id1 = id.Id1,
                id2 = id.Id2,
                id3 = id.Id3,
                id4 = id.Id4
            };
        }

        public static ConceptDTO ToConcept(this PBPublicId publicId) =>
            new ConceptDTO(publicId.ToPublicId());

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
            //# Tested
            List<PublicId> retVal = new List<PublicId>(value.PublicIds.Count);
            foreach (PBPublicId pbPublicId in value.PublicIds)
                retVal.Add(pbPublicId.ToPublicId());
            return retVal;
        }

        static HashSet<IPublicId> ToPublicIdHash(this PBPublicIdList value)
        {
            //# Tested
            HashSet<IPublicId> retVal = new HashSet<IPublicId>(value.PublicIds.Count);
            foreach (PBPublicId pbPublicId in value.PublicIds)
                retVal.Add(pbPublicId.ToPublicId());
            return retVal;
        }

        static ImmutableList<VertexDTO> ToVertexMap(this RepeatedField<PBVertex> value)
        {
            //# Tested
            ImmutableList<VertexDTO>.Builder bldr = ImmutableList<VertexDTO>.Empty.ToBuilder();
            foreach (PBVertex item in value)
                bldr.Add(item.ToVertex());
            return bldr.ToImmutableList();
        }

        static ImmutableDictionary<Int32, Int32> ToImmutableDictionary(this RepeatedField<PBIntToIntMap> value)
        {
            //# Tested
            ImmutableDictionary<Int32, Int32>.Builder bldr = ImmutableDictionary<Int32, Int32>.Empty.ToBuilder();
            foreach (PBIntToIntMap item in value)
                bldr.Add(item.Source, item.Target);
            return bldr.ToImmutableDictionary();
        }

        static ImmutableDictionary<Int32, ImmutableList<Int32>> ToImmutableDictionary(this RepeatedField<PBIntToMultipleIntMap> value)
        {
            //# Tested
            ImmutableDictionary<Int32, ImmutableList<Int32>>.Builder bldr = ImmutableDictionary<Int32, ImmutableList<Int32>>.Empty.ToBuilder();
            foreach (PBIntToMultipleIntMap item in value)
            {
                ImmutableList<Int32>.Builder l = ImmutableList<int>.Empty.ToBuilder();
                l.AddRange(item.Target);
                bldr.Add(item.Source, l.ToImmutableList());
            }
            return bldr.ToImmutableDictionary();
        }

        public static DiTreeDTO ToDiTree(this PBDiTree value)
        {
            //# Tested
            return new DiTreeDTO(
                value.Root.ToVertex(),
                value.PredecesorMap.ToImmutableDictionary(),
                value.VertexMap.ToVertexMap(),
                value.SuccessorMap.ToImmutableDictionary()
            );
        }

        public static DiGraphDTO ToDiGraph(this PBDiGraph value)
        {
            //# Tested
            return new DiGraphDTO(
                value.RootSequence.ToImmutableList(),
                value.PredecesorMap.ToImmutableDictionary(),
                value.VertexMap.ToVertexMap(),
                value.SuccessorMap.ToImmutableDictionary()
            );
        }

        public static GraphDTO ToGraph(this PBGraph value)
        {
            //# Tested
            return new GraphDTO(
                value.VertexMap.ToVertexMap(),
                value.SuccessorMap.ToImmutableDictionary()
            );
        }

        static ImmutableDictionary<IConcept, Object> ToImmutableDictionary(this RepeatedField<PBVertex.Types.Property> value)
        {
            //# Tested
            ImmutableDictionary<IConcept, Object>.Builder bldr = ImmutableDictionary<IConcept, Object>.Empty.ToBuilder();
            foreach (PBVertex.Types.Property item in value)
                bldr.Add(item.Concept.ToConcept(), item.Value.ToObject());
            return bldr.ToImmutableDictionary();
        }

        public static VertexDTO ToVertex(this PBVertex value)
        {
            //# Tested
            TinkarId tid = new TinkarId(value.VertexId.Id.Id1,
                value.VertexId.Id.Id2,
                value.VertexId.Id.Id3,
                value.VertexId.Id.Id4);
            return new VertexDTO(tid.Uuid,
                value.VertexIndex,
                value.Meaning.ToConcept(),
                value.Properties.ToImmutableDictionary());
            throw new NotImplementedException();
        }

        public static IEnumerable<Object> ToObjects(this IEnumerable<PBField> c)
        {
            //# Tested
            foreach (PBField f in c)
                yield return f.ToObject();
        }

        public static Object ToObject(this PBField f)
        {
            //# Tested
            switch (f.ValueCase)
            {
                case PBField.ValueOneofCase.StringValue:
                    return f.StringValue;
                case PBField.ValueOneofCase.BoolValue:
                    return f.BoolValue;
                case PBField.ValueOneofCase.IntValue:
                    return f.IntValue;
                case PBField.ValueOneofCase.FloatValue:
                    return f.FloatValue;
                case PBField.ValueOneofCase.BytesValue:
                    return f.BytesValue.ToByteArray();
                case PBField.ValueOneofCase.TimeValue:
                    return f.TimeValue.ToDateTime();
                case PBField.ValueOneofCase.ConceptValue:
                    return f.ConceptValue.ToConcept();
                case PBField.ValueOneofCase.PublicIdListValue:
                    return f.PublicIdListValue.ToPublicIdList();
                case PBField.ValueOneofCase.PublicIdHashValue:
                    return f.PublicIdHashValue.ToPublicIdHash();

                case PBField.ValueOneofCase.DiTreeValue:
                    return f.DiTreeValue.ToDiTree();

                case PBField.ValueOneofCase.DiGraphValue:
                    return f.DiGraphValue.ToDiGraph();

                case PBField.ValueOneofCase.VertexValue:
                    return f.VertexValue.ToVertex();

                case PBField.ValueOneofCase.GraphValue:
                    return f.GraphValue.ToGraph();

                default:
                    throw new NotImplementedException();
            }
        }

        #region Pattern
        public static PatternDTO ToPattern(this PBPattern c)
        {
            //# Tested
            return new PatternDTO(c.PublicId.ToPublicId());
        }

        static FieldDefinitionDTO ToFieldDefinition(this PBFieldDefinition field)
        {
            //# Tested
            return new FieldDefinitionDTO(
                field.DataType.ToPublicId(),
                field.Purpose.ToPublicId(),
                field.Meaning.ToPublicId()
            );
        }

        static IEnumerable<FieldDefinitionDTO> ToFieldDefinitions(this RepeatedField<PBFieldDefinition> fields)
        {
            //# Tested
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
            //# Tested
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
            //# Tested
            return new ConceptVersionDTO(c.PublicId.ToPublicId(), c.Stamp.ToStamp());
        }

        public static ConceptChronologyDTO ToConceptChronology(this PBConceptChronology c)
        {
            //# Tested
            ConceptVersionDTO[] versions = new ConceptVersionDTO[c.ConceptVersions.Count];
            for (Int32 i = 0; i < c.ConceptVersions.Count; i++)
                versions[i] = c.ConceptVersions[i].ToConceptVersion();
            return new ConceptChronologyDTO(c.PublicId.ToPublicId(), versions.ToImmutableArray());
        }
        #endregion

        public static IComponent Convert(PBTinkarMsg c)
        {
            switch (c.ValueCase)
            {
                case PBTinkarMsg.ValueOneofCase.ConceptValue:
                    return c.ConceptValue.ToConcept();
                case PBTinkarMsg.ValueOneofCase.ConceptVersionValue:
                    return c.ConceptVersionValue.ToConceptVersion();
                case PBTinkarMsg.ValueOneofCase.ConceptChronologyValue:
                    return c.ConceptChronologyValue.ToConceptChronology();

                case PBTinkarMsg.ValueOneofCase.PatternValue:
                    return c.PatternValue.ToPattern();
                case PBTinkarMsg.ValueOneofCase.PatternVersionValue:
                    return c.PatternVersionValue.ToPatternVersion();
                case PBTinkarMsg.ValueOneofCase.PatternChronologyValue:
                    return c.PatternChronologyValue.ToPatternChronology();

                case PBTinkarMsg.ValueOneofCase.SemanticValue:
                    return c.SemanticValue.ToSemantic();
                case PBTinkarMsg.ValueOneofCase.SemanticVersionValue:
                    return c.SemanticVersionValue.ToSemanticVersion();
                case PBTinkarMsg.ValueOneofCase.SemanticChronologyValue:
                    return c.SemanticChronologyValue.ToSemanticChronology();

                default:
                    throw new NotImplementedException();
            }
        }


        //public static IComponent Convert(Object c)
        //{
        //    switch (c)
        //    {
        //        case PBConcept item:
        //            return item.ToConcept();
        //        case PBConceptVersion item:
        //            return item.ToConceptVersion();
        //        case PBConceptChronology item:
        //            return item.ToConceptChronology();

        //        case PBPattern item:
        //            return item.ToPattern();
        //        case PBPatternVersion item:
        //            return item.ToPatternVersion();
        //        case PBPatternChronology item:
        //            return item.ToPatternChronology();

        //        case PBSemantic item:
        //            return item.ToSemantic();
        //        case PBSemanticVersion item:
        //            return item.ToSemanticVersion();
        //        case PBSemanticChronology item:
        //            return item.ToSemanticChronology();

        //        default:
        //            throw new NotImplementedException();
        //    }
        //}
    }
}
