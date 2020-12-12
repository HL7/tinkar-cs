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
    /// <typeparam name="T">Type of objects to check for equivalence.</typeparam>
    public interface IEquivalent<T>
    {
        /// <summary>
        /// Returns true if 'this' item is equivalent to 'other'
        /// item.
        /// </summary>
        /// <param name="other">other item to compare equivalence to.</param>
        /// <returns>true if two items are equivalent.</returns>
        bool IsEquivalent(T other);
    }
}
