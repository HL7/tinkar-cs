using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Tinkar.Common
{
    public interface IIntIdCollection : IIdCollection
    {

        //void forEach(IntConsumer consumer);

        //IntStream intStream();

        Int32 [] ToArray();

        Boolean Contains(int value);
    }
}
