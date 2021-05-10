using Google.Protobuf;
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
    public static class PBConvert
    {
        public static PBTinkarId ToPBTinkarId(this ITinkarId id)
        {
            //# Tested
            return new PBTinkarId
            {
                Id1 = (UInt32)id.Id1,
                Id2 = (UInt32)id.Id2,
                Id3 = (UInt32)id.Id3,
                Id4 = (UInt32)id.Id4
            };
        }

        public static PBVertexId ToPBVertexId(this IVertexId id)
        {
            //# Tested
            return new PBVertexId
            {
                Id = id.ToPBTinkarId()
            };
        }

        public static PBPublicId ToPBPublicId(this IPublicId publicId)
        {
            //# Tested
            PBPublicId retval = new PBPublicId();
            PBTinkarId[] tids = new PBTinkarId[publicId.UuidCount];
            for (Int32 i = 0; i < publicId.UuidCount; i++)
                tids[i] = publicId[i].ToPBTinkarId();
            retval.Id.AddRange(tids);
            return retval;
        }

        public static PBStamp ToPBStamp(this IStamp c)
        {
            //# Tested
            return new PBStamp
            {
                PublicId = c.PublicId.ToPBPublicId(),
                Status = c.Status.ToPBConcept(),
                Author = c.Author.ToPBConcept(),
                Module = c.Module.ToPBConcept(),
                Path = c.Path.ToPBConcept(),
                Time = Timestamp.FromDateTime(c.Time)
            };
        }

        #region Semantic
        public static PBSemantic ToPBSemantic(this SemanticDTO c)
        {
            //# Not Tested
            return new PBSemantic
            {
                PublicId = c.PublicId.ToPBPublicId(),
                ReferencedComponent = c.ReferencedComponentPublicId.ToPBPublicId(),
                PatternForSemantic = c.PatternForSemantic.ToPBPublicId()
            };
        }

        static IEnumerable<PBPublicId> ToPBPublicIds(this IEnumerable<IPublicId> ids)
        {
            foreach (IPublicId id in ids)
                yield return id.ToPBPublicId();
        }

        static IEnumerable<PBIntToIntMap> ToPBIntToIntMap(this ImmutableDictionary<Int32, Int32> map)
        {
            foreach (KeyValuePair<Int32, Int32> item in map)
            {
                yield return new PBIntToIntMap
                {
                    Source = item.Key,
                    Target = item.Value
                };
            }
        }

        public static PBDiTree ToPBDiTree(this DiTreeDTO value)
        {
            PBDiTree retVal = new PBDiTree();
            retVal.Root = value.Root.ToPBVertex();
            retVal.VertexMap.AddRange(value.VertexMap.ToPBVertices());
            retVal.PredecesorMap.AddRange(value.PredecessorMap.ToPBIntToIntMap());
            return retVal;
        }

        static IEnumerable<PBDiGraph.Types.Predecesors> ToDiGraphPredecesors(this ImmutableDictionary<Int32, ImmutableList<Int32>> map)
        {
            foreach (KeyValuePair<Int32, ImmutableList<Int32>> item in map)
            {
                PBDiGraph.Types.Predecesors retVal = new PBDiGraph.Types.Predecesors
                {
                    VertexIndex = item.Key,
                };
                retVal.PredecessorIndices.AddRange(item.Value);
                yield return retVal;
            }
        }

        static IEnumerable<PBDiGraph.Types.Successors> ToDiGraphSuccessors(this ImmutableDictionary<Int32, ImmutableList<Int32>> map)
        {
            foreach (KeyValuePair<Int32, ImmutableList<Int32>> item in map)
            {
                PBDiGraph.Types.Successors retVal = new PBDiGraph.Types.Successors
                {
                    VertexIndex = item.Key,
                };
                retVal.SuccessorIndices.AddRange(item.Value);
                yield return retVal;
            }
        }

        public static PBDiGraph ToPBDiGraph(this DiGraphDTO value)
        {
            PBDiGraph retVal = new PBDiGraph();
            retVal.RootSequence.Add(value.RootSequence.ToArray());
            retVal.VertexMap.AddRange(value.VertexMap.ToPBVertices());
            retVal.PredecesorMap.AddRange(value.PredecessorMap.ToDiGraphPredecesors());
            retVal.SuccessorMap.AddRange(value.SuccessorMap.ToDiGraphSuccessors());
            return retVal;
        }

        static IEnumerable<PBGraph.Types.Successors> ToGraphSuccessors(this ImmutableDictionary<Int32, ImmutableList<Int32>> map)
        {
            foreach (KeyValuePair<Int32, ImmutableList<Int32>> item in map)
            {
                PBGraph.Types.Successors retVal = new PBGraph.Types.Successors
                {
                    VertexIndex = item.Key,
                };
                retVal.SuccessorIndices.AddRange(item.Value);
                yield return retVal;
            }
        }

        public static PBGraph ToPBGraph(this GraphDTO value)
        {
            PBGraph retVal = new PBGraph();
            retVal.VertexMap.AddRange(value.VertexMap.ToPBVertices());
            retVal.SuccessorMap.AddRange(value.SuccessorMap.ToGraphSuccessors());
            return retVal;
        }




        public static PBVertex ToPBVertex(this VertexDTO value)
        {
            PBVertex retVal = new PBVertex
            {
                VertexId = value.VertexId.ToPBVertexId()
            };
            return retVal;
        }

        public static IEnumerable<PBVertex> ToPBVertices(this IEnumerable<VertexDTO> value)
        {
            foreach (VertexDTO v in value)
                yield return v.ToPBVertex();
        }

        static IEnumerable<PBField> ToPBFields(this IEnumerable<Object> items)
        {
            foreach (Object item in items)
            {
                PBField f = new PBField();
                switch (item)
                {
                    case String value:
                        f.StringValue = value;
                        break;
                    case Boolean value:
                        f.BoolValue = value;
                        break;
                    case Int32 value:
                        f.IntValue = value;
                        break;
                    case Single value:
                        f.FloatValue = value;
                        break;
                    case Byte[] value:
                        f.BytesValue = ByteString.CopyFrom(value);
                        break;
                    case ConceptDTO value:
                        f.ConceptValue = value.ToPBConcept();
                        break;
                    case DateTime value:
                        f.TimeValue = Timestamp.FromDateTime(value);
                        break;

                    case List<IPublicId> value:
                        {
                            PBPublicIdList l = new PBPublicIdList();
                            l.PublicIds.AddRange(value.ToPBPublicIds());
                            f.PublicIdListValue = l;
                        }
                        break;

                    case HashSet<IPublicId> value:
                        {
                            PBPublicIdList l = new PBPublicIdList();
                            l.PublicIds.AddRange(value.ToPBPublicIds());
                            f.PublicIdHashValue = l;
                        }
                        break;

                    case DiTreeDTO value:
                        f.DiTreeValue = value.ToPBDiTree();
                        break;

                    case DiGraphDTO value:
                        f.DiGraphValue = value.ToPBDiGraph();
                        break;

                    case GraphDTO value:
                        f.GraphValue = value.ToPBGraph();
                        break;

                    case VertexDTO value:
                        f.VertexValue = value.ToPBVertex();
                        break;

                    default:
                        throw new NotImplementedException();
                }
                yield return f;
            }
        }
        public static PBSemanticVersion ToPBSemanticVersion(this SemanticVersionDTO c)
        {
            //# Tested
            PBSemanticVersion retVal = new PBSemanticVersion
            {
                PublicId = c.PublicId.ToPBPublicId(),
                Stamp = c.Stamp.ToPBStamp(),
                ReferencedComponent = c.ReferencedComponentPublicId.ToPBPublicId(),
                PatternForSemantic = c.PatternForSemantic.ToPBPublicId()
            };
            retVal.FieldValues.AddRange(c.Fields.ToPBFields());
            return retVal;
        }

        public static PBSemanticChronology ToPBSemanticChronology(this SemanticChronologyDTO c)
        {
            //# Tested
            PBSemanticChronology retVal = new PBSemanticChronology
            {
                PublicId = c.PublicId.ToPBPublicId(),
                PatternForSemantic = c.PatternForSemantic.ToPBPublicId(),
                ReferencedComponent = c.ReferencedComponentPublicId.ToPBPublicId()
            };

            PBSemanticVersion[] versions = new PBSemanticVersion[c.Versions.Length];
            for (Int32 i = 0; i < c.Versions.Length; i++)
                versions[i] = c.Versions[i].ToPBSemanticVersion();
            retVal.Versions.AddRange(versions);

            return retVal;
        }
        #endregion

        #region Concept
        public static PBConcept ToPBConcept(this IConcept c)
        {
            //# Tested
            return new PBConcept
            {
                PublicId = c.PublicId.ToPBPublicId()
            };
        }

        public static PBConceptVersion ToPBConceptVersion(this ConceptVersionDTO c)
        {
            //# Not Tested
            return new PBConceptVersion
            {
                PublicId = c.PublicId.ToPBPublicId(),
                Stamp = c.StampDTO.ToPBStamp()
            };
        }

        public static PBConceptChronology ToPBConceptChronology(this ConceptChronologyDTO c)
        {
            //# Not Tested
            PBConceptChronology retVal = new PBConceptChronology
            {
                PublicId = c.PublicId.ToPBPublicId()
            };

            PBConceptVersion[] versions = new PBConceptVersion[c.Versions.Length];
            for (Int32 i = 0; i < c.Versions.Length; i++)
                versions[i] = c.Versions[i].ToPBConceptVersion();
            retVal.ConceptVersions.AddRange(versions);
            return retVal;
        }
        #endregion

        public static object Convert(IComponent c)
        {
            switch (c)
            {
                case ConceptDTO item: return item.ToPBConcept();
                case ConceptVersionDTO item: return item.ToPBConceptVersion();
                case ConceptChronologyDTO item: return item.ToPBConceptChronology();

                case SemanticDTO item: return item.ToPBSemantic();
                case SemanticVersionDTO item: return item.ToPBSemanticVersion();
                case SemanticChronologyDTO item: return item.ToPBSemanticChronology();

                default:
                    //throw new NotImplementedException();
                    return null;
            }
        }
    }
}
