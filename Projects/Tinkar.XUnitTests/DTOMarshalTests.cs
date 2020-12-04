using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public class DTOMarshalTests
    {
        static byte[] zero => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        Guid g1 = new Guid(1, 0, 0, zero);
        Guid g2 = new Guid(2, 0, 0, zero);
        Guid g3 = new Guid(3, 0, 0, zero);
        Guid g4 = new Guid(4, 0, 0, zero);

        void Compare(IEnumerable<Guid> inGuids, params Guid[] cmpGuids)
        {
            Guid[] guidArr = inGuids.ToArray();
            Assert.True(guidArr.Length == cmpGuids.Length);
            for (Int32 i = 0; i < guidArr.Length; i++)
                Assert.True(guidArr[i].CompareTo(cmpGuids[i]) == 0);
        }

        [Fact]
        public void ConceptDTOTest()
        {
            ConceptDTO dtoStart = new ConceptDTO(new Guid[] { this.g1, this.g2, this.g3, this.g4 });

            Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);

            MemoryStream ms = new MemoryStream();
            TinkarOutput output = new TinkarOutput(ms);
            dtoStart.Marshal(output);

            ms.Position = 0;
            TinkarInput input = new TinkarInput(ms);
            ConceptDTO dtoRead = ConceptDTO.Make(input);
            Assert.True(dtoStart.Equals(dtoRead));
        }
    }
}
