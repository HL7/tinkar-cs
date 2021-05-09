using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinkar.Dto;
using Tinkar.ProtoBuf.CS;

namespace Tinkar.XUnitTests
{
    public static class PBConvert
    {
        public static PBTinkarId Convert(ITinkarId id)
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

        public static PBPublicId Convert(IPublicId publicId)
        {
            //# Tested
            PBPublicId retval = new PBPublicId();
            PBTinkarId[] tids = new PBTinkarId[publicId.UuidCount];
            for (Int32 i = 0; i < publicId.UuidCount; i++)
                tids[i] = Convert(publicId[i]);
            retval.Id.AddRange(tids);
            return retval;
        }

        public static PBStamp Convert(IStamp c)
        {
            //# Tested
            return new PBStamp
            {
                PublicId = Convert(c.PublicId),
                Status = Convert(c.Status),
                Author = Convert(c.Author),
                Module = Convert(c.Module),
                Path = Convert(c.Path),
                Time = Timestamp.FromDateTime(c.Time)
            };
        }

        #region Semantic
        public static PBSemantic Convert(SemanticDTO c)
        {
            //# Not Tested
            return new PBSemantic
            {
                PublicId = Convert(c.PublicId),
                ReferencedComponent = Convert(c.ReferencedComponentPublicId),
                PatternForSemantic = Convert(c.PatternForSemantic)
            };
        }

        static IEnumerable<PBField> Convert(IEnumerable<Object> items)
        {
            foreach (Object item in items)
            {
                switch (item)
                {
                    case String value: 
                        yield return new PBField { StringValue = value };
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        public static PBSemanticVersion Convert(SemanticVersionDTO c)
        {
            //# Tested
            PBSemanticVersion retVal = new PBSemanticVersion
            {
                PublicId = Convert(c.PublicId),
                Stamp = Convert(c.Stamp),
                ReferencedComponent = Convert(c.ReferencedComponentPublicId),
                PatternForSemantic = Convert(c.PatternForSemantic)
            };
            retVal.FieldValues.AddRange(Convert(c.Fields));
            return retVal;
        }

        public static PBSemanticChronology Convert(SemanticChronologyDTO c)
        {
            //# Tested
            PBSemanticChronology retVal = new PBSemanticChronology
            {
                PublicId = Convert(c.PublicId),
                PatternForSemantic = Convert(c.PatternForSemantic),
                ReferencedComponent = Convert(c.ReferencedComponentPublicId)
            };

            PBSemanticVersion[] versions = new PBSemanticVersion[c.Versions.Length];
            for (Int32 i = 0; i < c.Versions.Length; i++)
                versions[i] = Convert(c.Versions[i]);
            retVal.Versions.AddRange(versions);

            return retVal;
        }
        #endregion

        #region Concept
        public static PBConcept Convert(IConcept c)
        {
            //# Tested
            return new PBConcept
            {
                PublicId = Convert(c.PublicId)
            };
        }

        public static PBConceptVersion Convert(ConceptVersionDTO c)
        {
            //# Not Tested
            return new PBConceptVersion
            {
                PublicId = Convert(c.PublicId),
                Stamp = Convert(c.StampDTO)
            };
        }

        public static PBConceptChronology Convert(ConceptChronologyDTO c)
        {
            //# Not Tested
            PBConceptChronology retVal = new PBConceptChronology
            {
                PublicId = Convert(c.PublicId)
            };

            PBConceptVersion[] versions = new PBConceptVersion[c.Versions.Length];
            for (Int32 i = 0; i < c.Versions.Length; i++)
                versions[i] = Convert(c.Versions[i]);
            retVal.ConceptVersions.AddRange(versions);
            return retVal;
        }
        #endregion

        public static object Convert(IComponent c)
        {
            switch (c)
            {
                case ConceptDTO item:
                    return Convert(item);
                case ConceptVersionDTO item:
                    return Convert(item);
                case ConceptChronologyDTO item:
                    return Convert(item);

                case SemanticDTO item:
                    return Convert(item);
                case SemanticVersionDTO item:
                    return Convert(item);
                case SemanticChronologyDTO item:
                    return Convert(item);

                default:
                    //throw new NotImplementedException();
                    return null;
            }
        }
    }
}
