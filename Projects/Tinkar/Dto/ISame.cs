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
    public interface ISame
    {
        /// <summary>
        /// Returns true if 'this' item and 'other' item are identical in values.
        /// This is a deep comparison.
        /// item.
        /// </summary>
        /// <param name="other">other item to compare equivalence to.</param>
        /// <returns>true if two items are same.</returns>
        Int32 IsSame(Object other);
    }

    public interface ISame<T>
    {
        /// <summary>
        /// Returns true if 'this' item and 'other' item are identical in values.
        /// This is a deep comparison.
        /// item.
        /// </summary>
        /// <param name="other">other item to compare equivalence to.</param>
        /// <returns>true if two items are same.</returns>
        Int32 IsSame(T other);
    }
}
