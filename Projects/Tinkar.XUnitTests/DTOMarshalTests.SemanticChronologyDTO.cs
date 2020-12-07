﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Tinkar.XUnitTests
{
    public partial class DTOMarshalTests
    {
    //    [Fact]
    //    public void SemanticChronologyDTOFieldsTest()
    //    {
    //        SemanticChronologyDTO dtoStart = new SemanticChronologyDTO(
    //            new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //            new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //            new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //        Compare(dtoStart.ComponentUuids, this.g1, this.g2, this.g3, this.g4);
    //        Compare(dtoStart.DefinitionForSemanticUuids, this.h1, this.h2, this.h3, this.h4);
    //        Compare(dtoStart.ReferencedComponentUuids, this.i1, this.i2, this.i3, this.i4);
    //    }

    //    [Fact]
    //    public void SemanticChronologyDTOIsEquivalentTest()
    //    {
    //        {
    //            SemanticChronologyDTO a = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            SemanticChronologyDTO b = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            Assert.True(a.IsEquivalent(b));
    //        }

    //        {
    //            SemanticChronologyDTO a = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            SemanticChronologyDTO b = new SemanticChronologyDTO(
    //                new Guid[] { this.g2, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            Assert.False(a.IsEquivalent(b));
    //        }

    //        {
    //            SemanticChronologyDTO a = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            SemanticChronologyDTO b = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h3, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            Assert.False(a.IsEquivalent(b));
    //        }
            
    //        {
    //            SemanticChronologyDTO a = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //            );
    //            SemanticChronologyDTO b = new SemanticChronologyDTO(
    //                new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //                new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //                new Guid[] { this.i1, this.i2, this.i3, this.i3 }
    //            );
    //            Assert.False(a.IsEquivalent(b));
    //        }
    //    }

    //    [Fact]
    //    public void SemanticChronologyDTOMarshalTest()
    //    {
    //        SemanticChronologyDTO dtoStart = new SemanticChronologyDTO(
    //            new Guid[] { this.g1, this.g2, this.g3, this.g4 },
    //            new Guid[] { this.h1, this.h2, this.h3, this.h4 },
    //            new Guid[] { this.i1, this.i2, this.i3, this.i4 }
    //        );

    //        MemoryStream ms = new MemoryStream();
    //        TinkarOutput output = new TinkarOutput(ms);
    //        output.WriteField(dtoStart);

    //        ms.Position = 0;
    //        TinkarInput input = new TinkarInput(ms);
    //        SemanticChronologyDTO dtoRead = (SemanticChronologyDTO)input.ReadField();
    //        Assert.True(dtoStart.IsEquivalent(dtoRead));
    //    }
    }
}