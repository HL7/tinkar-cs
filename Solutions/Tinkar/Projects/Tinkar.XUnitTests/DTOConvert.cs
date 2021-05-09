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
        public static TinkarId Convert(PBTinkarId id)
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

        public static PublicId Convert(PBPublicId publicId)
        {
            //# Tested
            Guid[] tids = new Guid[publicId.Id.Count];
            for (Int32 i = 0; i < publicId.Id.Count; i++)
                tids[i] = Convert(publicId.Id[i]).uuid;
            return new PublicId(tids);
        }

        public static StampDTO Convert(PBStamp c)
        {
            //# Tested
            return new StampDTO(
                Convert(c.PublicId),
                Convert(c.Status).PublicId,
                c.Time.ToDateTime(),
                Convert(c.Author).PublicId,
                Convert(c.Module).PublicId,
                Convert(c.Path).PublicId);
        }

        #region Semantic
        public static SemanticDTO Convert(PBSemantic c)
        {
            //# Not Tested
            return new SemanticDTO(
                Convert(c.PublicId),
                Convert(c.PatternForSemantic),
                Convert(c.ReferencedComponent));
        }

        public static IEnumerable<Object> Convert(IEnumerable<PBField> c)
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
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public static SemanticVersionDTO Convert(PBSemanticVersion c)
        {
            //# Tested
            return new SemanticVersionDTO(
                Convert(c.PublicId),
                Convert(c.PatternForSemantic),
                Convert(c.ReferencedComponent),
                Convert(c.Stamp),
                Convert(c.FieldValues).ToImmutableArray()
                );
        }

        public static SemanticChronologyDTO Convert(PBSemanticChronology c)
        {
            //# Tested
            SemanticVersionDTO[] versions = new SemanticVersionDTO[c.Versions.Count];
            for (Int32 i = 0; i < c.Versions.Count; i++)
                versions[i] = Convert(c.Versions[i]);

            SemanticChronologyDTO retVal = new SemanticChronologyDTO(
                Convert(c.PublicId),
                Convert(c.PatternForSemantic),
                Convert(c.ReferencedComponent),
                versions.ToImmutableArray());
            return retVal;
        }
        #endregion

        #region Concept
        public static ConceptDTO Convert(PBConcept c)
        {
            //# Tested
            return new ConceptDTO(Convert(c.PublicId));
        }

        public static ConceptVersionDTO Convert(PBConceptVersion c)
        {
            //# Not Tested
            return new ConceptVersionDTO(Convert(c.PublicId), Convert(c.Stamp));
        }

        public static ConceptChronologyDTO Convert(PBConceptChronology c)
        {
            //# Not Tested
            ConceptVersionDTO[] versions = new ConceptVersionDTO[c.ConceptVersions.Count];
            for (Int32 i = 0; i < c.ConceptVersions.Count; i++)
                versions[i] = Convert(c.ConceptVersions[i]);
            return new ConceptChronologyDTO(Convert(c.PublicId), versions.ToImmutableArray());
        }
        #endregion

        public static IComponent Convert(Object c)
        {
            switch (c)
            {
                case PBConcept item:
                    return Convert(item);
                case PBConceptVersion item:
                    return Convert(item);
                case PBConceptChronology item:
                    return Convert(item);

                case PBSemantic item:
                    return Convert(item);
                case PBSemanticVersion item:
                    return Convert(item);
                case PBSemanticChronology item:
                    return Convert(item);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
