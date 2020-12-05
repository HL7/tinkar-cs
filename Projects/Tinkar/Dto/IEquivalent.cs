using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// IEquivalent.
    /// Tests if two items are the 'same'. This looks at actual values,
    /// not references, to determine equivalency.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEquivalent<T>
    {
        bool IsEquivalent(T other);
    }
}
