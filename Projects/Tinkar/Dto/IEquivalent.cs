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
    public interface IEquivalent
    {
        /// <summary>
        /// Returns true if 'this' item is equivalent to 'other'
        /// item.
        /// </summary>
        /// <param name="other">other item to compare equivalence to.</param>
        /// <returns>true if two items are equivalent.</returns>
        bool IsEquivalent(Object other);
    }
}
